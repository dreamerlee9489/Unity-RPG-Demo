using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using App.SO;
using App.Manager;
using App.Items;
using App.UI;

namespace App.Control
{
    public class CombatEntity : MonoBehaviour, ICmdReceiver, IMsgReceiver
    {
        Animator animator = null;
        NavMeshAgent agent = null;
        Weapon weapon = null;
        Item pickup = null;
        public Transform weaponPos = null;
        public Weapon weaponPrefab = null;
        public EntityConfig entityConfig = null;
        public ProgressionConfig progressionConfig = null;
        public DropListConfig dropListConfig = null;
        public int level = 1;
        public float currentHP { get; set; }
        public float currentMP { get; set; }
        public float currentEXP { get; set; }
        public float currentDEF { get; set; }
        public float currentATK { get; set; }
        public float maxHP { get; set; }
        public float maxMP { get; set; }
        public float maxEXP { get; set; }
        public float maxDEF { get; set; }
        public float maxATK { get; set; }
        public bool isDead { get; set; }
        public float sqrViewRadius { get; set; }
        public float sqrAttackRadius { get; set; }
        public Transform target { get; set; }
        public HUDBar hpBar { get; set; }
        public Progression progression { get; set; }

        void Awake()
        {
            GetComponent<Collider>().enabled = true;
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = entityConfig.stopDistance;
            agent.radius = 0.5f;
            sqrViewRadius = Mathf.Pow(entityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
            progression = progressionConfig.GetProgressionByLevel(level);
            maxHP = progression.thisLevelHP;
            maxMP = progression.thisLevelMP;
            maxATK = progression.thisLevelATK + (weaponPrefab == null ? 0 : (weaponPrefab.itemConfig as WeaponConfig).atk);
            maxDEF = progression.thisLevelDEF;
            maxEXP = progression.upLevelEXP;
            currentHP = progression.thisLevelHP;
            currentMP = progression.thisLevelMP;
            currentATK = progression.thisLevelATK + (weaponPrefab == null ? 0 : (weaponPrefab.itemConfig as WeaponConfig).atk);
            currentDEF = progression.thisLevelDEF;
            currentEXP = 0;
        }

        void Start()
        {
            if (weaponPrefab != null)
            {
                AttachEquipment(weapon = Instantiate(weaponPrefab, weaponPos));
                weapon.collider.enabled = false;
                weapon.rigidbody.useGravity = false;
                weapon.rigidbody.isKinematic = true;
            }
            hpBar = CompareTag("Player") ? UIManager.Instance.hudPanel.hpBar : transform.GetChild(0).GetComponent<HUDBar>();
        }

        void Pickup()
        {
            for (int i = 0; i < GameManager.Instance.registeredTasks.Count; i++)
            {
                Item temp = GameManager.Instance.registeredTasks[i].target.GetComponent<Item>();
                if (temp != null && pickup.Equals(temp))
                    GameManager.Instance.registeredTasks[i].UpdateProgress(1);
            }
            pickup.AddToInventory();
            if (pickup.nameBar != null)
            {
                Destroy(pickup.nameBar.gameObject);
                pickup.nameBar = null;
            }
            Destroy(pickup.gameObject);
            animator.SetBool("pickup", false);
            UIManager.Instance.messagePanel.ShowMessage("[系统]  你拾取了" + pickup.itemConfig.itemName + " * 1", Color.green);
        }

        void AttackL(float factor) => TakeDamage(target, factor);
        void AttackR(float factor) => TakeDamage(target, factor);
        void TakeDamage(Transform target, float factor = 1)
        {
            if (target != null)
            {
                CombatEntity defender = target.GetComponent<CombatEntity>();
                defender.currentHP = Mathf.Max(defender.currentHP - Mathf.Max(currentATK * factor - defender.currentDEF, 1), 0);
                defender.hpBar.UpdateBar(new Vector3(defender.currentHP / defender.maxHP, 1, 1));
                if (defender.currentHP <= 0)
                {
                    defender.Death();
                    CancelAction();
                }
            }
        }

        void Death()
        {
            isDead = true;
            target = null;
            animator.SetBool("attack", false);
            animator.SetBool("death", true);
            agent.radius = 0;
            GetComponent<Collider>().enabled = false;
            List<Item> drops = dropListConfig.GetDrops(progression, ref InventoryManager.Instance.playerData.golds);
            UIManager.Instance.goldPanel.UpdatePanel();
            foreach (var item in drops)
                Instantiate(item, transform.position + Vector3.up * 2 + Random.insideUnitSphere, Quaternion.Euler(90, 90, 90));
            if (CompareTag("Enemy"))
            {
                for (int i = 0; i < GameManager.Instance.registeredTasks.Count; i++)
                {
                    CombatEntity entity = GameManager.Instance.registeredTasks[i].target.GetComponent<CombatEntity>();
                    if (entity != null && entity.entityConfig.nickName == entityConfig.nickName)
                        GameManager.Instance.registeredTasks[i].UpdateProgress(1);
                }
                UIManager.Instance.hudPanel.xpBar.UpdateBar(new Vector3(GameManager.Instance.player.currentEXP / GameManager.Instance.player.maxEXP, 1, 1));
                GameManager.Instance.player.GetExprience(progression.upLevelEXP * 0.5f);
            }
        }

        public void GetExprience(float exp)
        {
            currentEXP += exp;
            if (currentEXP >= maxEXP)
            {
                currentEXP -= maxEXP;
                progression = progressionConfig.GetProgressionByLevel(++level);
                maxHP = progression.thisLevelHP;
                maxMP = progression.thisLevelMP;
                maxDEF = progression.thisLevelDEF;
                maxATK = progression.thisLevelATK + (weapon == null ? 0 : (weapon.itemConfig as WeaponConfig).atk);
                maxEXP = progression.upLevelEXP;
                currentHP += progression.thisLevelHP * 0.2f;
                currentMP += progression.thisLevelMP * 0.2f;
                currentDEF = progression.thisLevelDEF;
                currentATK = progression.thisLevelATK + (weapon == null ? 0 : (weapon.itemConfig as WeaponConfig).atk);
                UIManager.Instance.hudPanel.hpBar.UpdateBar(new Vector3(currentHP / progression.thisLevelHP, 1, 1));
                UIManager.Instance.hudPanel.mpBar.UpdateBar(new Vector3(currentMP / progression.thisLevelMP, 1, 1));
                UIManager.Instance.hudPanel.xpBar.UpdateBar(new Vector3(currentEXP / progression.upLevelEXP, 1, 1));
                UIManager.Instance.attributePanel.UpdatePanel();
            }
        }

        public void AttachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currentATK = progression.thisLevelATK + weaponConfig.atk;
                    animator.runtimeAnimatorController = weaponConfig.animatorController;
                    equipment.transform.SetParent(weaponPos);
                    equipment.gameObject.SetActive(true);
                    weapon = equipment as Weapon;
                    break;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.itemConfig as ArmorConfig;
                    currentHP = progression.thisLevelATK + armorConfig.hp;
                    currentDEF = progression.thisLevelDEF + armorConfig.def;
                    break;
                case EquipmentType.JEWELRY:
                    break;
            }
            equipment.containerType = ContainerType.EQUIPMENT;
        }

        public void DetachEquipment(Equipment equipment, ContainerType containerType = ContainerType.BAG)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currentATK = progression.thisLevelATK;
                    animator.runtimeAnimatorController = Resources.LoadAsync("Animator/Unarmed Controller").asset as RuntimeAnimatorController;
                    equipment.transform.SetParent(InventoryManager.Instance.inventory);
                    equipment.gameObject.SetActive(false);
                    weapon = null;
                    break;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.itemConfig as ArmorConfig;
                    currentHP = progression.thisLevelHP;
                    currentDEF = progression.thisLevelDEF;
                    break;
                case EquipmentType.JEWELRY:
                    break;
            }
            equipment.containerType = containerType;
        }

        public void ExecuteAction(Vector3 point) { }
        public void ExecuteAction(Transform target)
        {
            this.target = target;
            transform.LookAt(target);
            if ((pickup = target.GetComponent<Item>()) != null)
            {
                if (CanDialogue(pickup.transform))
                {
                    animator.SetBool("pickup", true);
                    this.target = null;
                }
                else
                    agent.destination = target.position;
            }
            else
            {
                if (CanAttack(target))
                    animator.SetBool("attack", true);
                else
                {
                    agent.destination = target.position;
                    animator.SetBool("attack", false);
                }
            }
        }

        public void CancelAction()
        {
            target = null;
            pickup = null;
            animator.SetBool("attack", false);
            animator.SetBool("pickup", false);
        }

        public bool CanSee(Transform target)
        {
            if (!target.GetComponent<CombatEntity>().isDead)
            {
                Vector3 direction = target.position - transform.position;
                if (direction.sqrMagnitude <= sqrViewRadius)
                    return true;
            }
            return false;
        }

        public bool CanAttack(Transform target)
        {
            if (!target.GetComponent<CombatEntity>().isDead)
            {
                Vector3 direction = target.position - transform.position;
                if (direction.sqrMagnitude <= sqrAttackRadius && Vector3.Dot(transform.forward, direction.normalized) > 0.5f)
                    return true;
            }
            return false;
        }

        public bool CanDialogue(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude <= 2.25f)
                return true;
            return false;
        }

        public bool HandleMessage(Telegram telegram)
        {
            print(Time.unscaledTime + "s: " + gameObject.name + " recv: " + telegram.ToString());
            return GetComponent<FSM.FiniteStateMachine>().HandleMessage(telegram);
        }
    }
}
