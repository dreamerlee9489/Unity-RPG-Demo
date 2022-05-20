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
        public Transform weaponPos = null;
        public Weapon weapon = null;
        public HUDBar healthBar = null;
        public EntityConfig entityConfig = null;
        public ProgressionConfig progressionConfig = null;
        public DropListConfig dropListConfig = null;
        public int level { get; set; }
        public float currentHp { get; set; }
        public float currentMp { get; set; }
        public float currentExp { get; set; }
        public float currentDef { get; set; }
        public float currentAtk { get; set; }
        public float maxHp { get; set; }
        public float maxMp { get; set; }
        public float maxExp { get; set; }
        public float maxDef { get; set; }
        public float maxAtk { get; set; }
        public bool isDead { get; set; }
        public float sqrViewRadius { get; set; }
        public float sqrAttackRadius { get; set; }
        public Transform combatTarget { get; set; }
        public Progression progression { get; set; }

        void Awake()
        {
            if (!CompareTag("Player"))
                healthBar = transform.GetChild(0).GetComponent<HUDBar>();
            GetComponent<Collider>().isTrigger = true;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            GetComponent<Collider>().enabled = true;
            agent.stoppingDistance = entityConfig.stopDistance;
            agent.radius = 0.5f;
            sqrViewRadius = Mathf.Pow(entityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
            level = 1;
            progression = progressionConfig.GetProgressionByLevel(level);
            maxHp = progression.thisLevelHp;
            maxMp = progression.thisLevelMp;
            maxAtk = progression.thisLevelAtk + (weapon == null ? 0 : (weapon.itemConfig as WeaponConfig).atk);
            maxDef = progression.thisLevelDef;
            maxExp = progression.upLevelExp;
            currentHp = progression.thisLevelHp;
            currentMp = progression.thisLevelMp;
            currentAtk = progression.thisLevelAtk + (weapon == null ? 0 : (weapon.itemConfig as WeaponConfig).atk);
            currentDef = progression.thisLevelDef;
            currentExp = 0;
        }

        void AttackL(float factor) => TakeDamage(combatTarget, factor);
        void AttackR(float factor) => TakeDamage(combatTarget, factor);
        void TakeDamage(Transform target, float factor = 1)
        {
            if (target != null)
            {
                CombatEntity defender = target.GetComponent<CombatEntity>();
                defender.currentHp = Mathf.Max(defender.currentHp - Mathf.Max(currentAtk * factor - defender.currentDef, 1), 0);
                defender.healthBar.UpdateBar(new Vector3(defender.currentHp / defender.maxHp, 1, 1));
                if (defender.currentHp <= 0)
                {
                    defender.Death();
                    CancelAction();
                }
            }
            if (this == GameManager.Instance.player)
                UIManager.Instance.attributePanel.UpdatePanel();
        }

        void Death()
        {
            isDead = true;
            combatTarget = null;
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
                UIManager.Instance.hudPanel.xpBar.UpdateBar(new Vector3(GameManager.Instance.player.currentExp / GameManager.Instance.player.maxExp, 1, 1));
                if (GameManager.Instance.player.currentExp >= GameManager.Instance.player.maxHp)
                    GameManager.Instance.player.GetExprience(progression.upLevelExp * 1.5f);
            }
        }

        public void GetExprience(float exp)
        {
            currentExp += progression.upLevelExp * 1.5f;
            if (currentExp >= maxExp)
            {
                currentExp -= maxExp;
                progression = progressionConfig.GetProgressionByLevel(++level);
                maxHp = progression.thisLevelHp;
                maxMp = progression.thisLevelMp;
                maxDef = progression.thisLevelDef;
                maxAtk = progression.thisLevelAtk + (weapon == null ? 0 : (weapon.itemConfig as WeaponConfig).atk);
                maxExp = progression.upLevelExp;
                currentHp += progression.thisLevelHp * 0.2f;
                currentMp += progression.thisLevelMp * 0.2f;
                currentDef = progression.thisLevelDef;
                currentAtk = progression.thisLevelAtk + (weapon == null ? 0 : (weapon.itemConfig as WeaponConfig).atk);
                UIManager.Instance.hudPanel.hpBar.UpdateBar(new Vector3(currentHp / progression.thisLevelHp, 1, 1));
                UIManager.Instance.hudPanel.mpBar.UpdateBar(new Vector3(currentMp / progression.thisLevelMp, 1, 1));
                UIManager.Instance.hudPanel.xpBar.UpdateBar(new Vector3(currentExp / progression.upLevelExp, 1, 1));
                UIManager.Instance.attributePanel.UpdatePanel();
            }
        }

        public void AttachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currentAtk = progression.thisLevelAtk + weaponConfig.atk;
                    weapon = equipment as Weapon;
                    animator.runtimeAnimatorController = weaponConfig.animatorController;
                    equipment.transform.SetParent(weaponPos);
                    equipment.gameObject.SetActive(true);
                    break;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.itemConfig as ArmorConfig;
                    currentHp = progression.thisLevelAtk + armorConfig.hp;
                    currentDef = progression.thisLevelDef + armorConfig.def;
                    break;
                case EquipmentType.JEWELRY:
                    break;
            }
            UIManager.Instance.attributePanel.UpdatePanel();
        }

        public void DetachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currentAtk = progression.thisLevelAtk;
                    weapon = null;
                    animator.runtimeAnimatorController = Resources.LoadAsync("Animator/Unarmed Controller").asset as RuntimeAnimatorController;
                    equipment.transform.SetParent(InventoryManager.Instance.inventory);
                    equipment.gameObject.SetActive(false);
                    break;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.itemConfig as ArmorConfig;
                    currentHp = progression.thisLevelHp;
                    currentDef = progression.thisLevelDef;
                    break;
                case EquipmentType.JEWELRY:
                    break;
            }
            UIManager.Instance.attributePanel.UpdatePanel();
        }

        public void ExecuteAction(Vector3 point) { }
        public void ExecuteAction(Transform target)
        {
            if (!target.GetComponent<CombatEntity>().isDead)
            {
                this.combatTarget = target;
                transform.LookAt(target);
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
            combatTarget = null;
            animator.SetBool("attack", false);
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

        public bool HandleMessage(Telegram telegram)
        {
            print(Time.unscaledTime + "s: " + gameObject.name + " recv: " + telegram.ToString());
            return GetComponent<FSM.FiniteStateMachine>().HandleMessage(telegram);
        }
    }
}
