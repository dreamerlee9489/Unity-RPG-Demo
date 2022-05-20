using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using App.Config;
using App.Manager;
using App.Items;
using App.UI;

namespace App.Control
{
    public class CombatEntity : MonoBehaviour, ICmdReceiver, IMsgReceiver
    {
        Animator animator = null;
        NavMeshAgent agent = null;
        public Weapon unarmedWeapon = null;
        public Weapon weapon = null;
        public HUDBar healthBar = null;
        public int level = 1;
        public float currHp { get; set; }
        public float currMp { get; set; }
        public float currDef { get; set; }
        public float currAtk { get; set; }
        public float currExp { get; set; }
        public bool isDead { get; set; }
        public float sqrViewRadius { get; set; }
        public float sqrAttackRadius { get; set; }
        public Transform combatTarget { get; set; }
        public Progression progression { get; set; }
        public EntityConfig entityConfig = null;
        public ProgressionConfig progressionConfig = null;
        public DropListConfig dropListConfig = null;

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
            progression = progressionConfig.GetProgressionByLevel(level);
            currHp = progression.thisLevelHp;
            currMp = progression.thisLevelMp;
            currDef = progression.thisLevelDef;
            currAtk = progression.thisLevelAtk + (weapon.itemConfig as WeaponConfig).atk;
            currExp = 0;
        }

        void AttackL(float factor) => TakeDamage(combatTarget, factor);
        void AttackR(float factor) => TakeDamage(combatTarget, factor);

        void TakeDamage(Transform target, float factor = 1)
        {
            if (target != null)
            {
                CombatEntity defender = target.GetComponent<CombatEntity>();
                defender.currHp = Mathf.Max(defender.currHp - Mathf.Max(currAtk * factor - defender.currDef, 1), 0);
                defender.healthBar.UpdateBar(new Vector3(defender.currHp / defender.progression.thisLevelHp, 1, 1));
                if (defender.currHp <= 0)
                {
                    defender.Death();
                    CancelAction();
                }
            }
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
                for (int i = 0; i < GameManager.Instance.registeredQuests.Count; i++)
                {
                    CombatEntity entity = GameManager.Instance.registeredQuests[i].target.GetComponent<CombatEntity>();
                    if (entity != null && entity.entityConfig.nickName == entityConfig.nickName)
                        GameManager.Instance.registeredQuests[i].UpdateProgress(1);
                }
                GameManager.Instance.player.currExp += progression.nextLevelExp * 1.5f;
                UIManager.Instance.hudPanel.xpBar.UpdateBar(new Vector3(GameManager.Instance.player.currExp / GameManager.Instance.player.progression.nextLevelExp, 1, 1));
                if (GameManager.Instance.player.currExp >= GameManager.Instance.player.progression.nextLevelExp)
                    GameManager.Instance.player.LevelUp();
            }
        }

        void LevelUp()
        {
            currExp = currExp - progression.nextLevelExp;
            progression = progressionConfig.GetProgressionByLevel(++level);
            currHp = progression.thisLevelHp;
            currMp = progression.thisLevelMp;
            currDef = progression.thisLevelDef;
            currAtk = progression.thisLevelAtk + (weapon.itemConfig as WeaponConfig).atk;
            UIManager.Instance.hudPanel.hpBar.UpdateBar(new Vector3(currHp / progression.thisLevelHp, 1, 1));
            UIManager.Instance.hudPanel.mpBar.UpdateBar(new Vector3(currMp / progression.thisLevelMp, 1, 1));
            UIManager.Instance.hudPanel.xpBar.UpdateBar(new Vector3(currExp / progression.nextLevelExp, 1, 1));
        }

        public void AttachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currAtk = progression.thisLevelAtk + weaponConfig.atk;
                    weapon = equipment as Weapon;
                    animator.runtimeAnimatorController = weaponConfig.animatorController;
                    equipment.transform.SetParent(unarmedWeapon.transform);
                    equipment.gameObject.SetActive(true);
                    return;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.itemConfig as ArmorConfig;
                    currHp = progression.thisLevelAtk + armorConfig.hp;
                    currDef = progression.thisLevelDef + armorConfig.def;
                    return;
                case EquipmentType.JEWELRY:
                    return;
            }
        }

        public void DetachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currAtk = progression.thisLevelAtk;
                    weapon = unarmedWeapon;
                    animator.runtimeAnimatorController = Resources.LoadAsync("Animator/Unarmed Controller").asset as RuntimeAnimatorController;
                    equipment.transform.SetParent(InventoryManager.Instance.inventory);
                    equipment.gameObject.SetActive(false);
                    return;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.itemConfig as ArmorConfig;
                    currHp = progression.thisLevelHp;
                    currDef = progression.thisLevelDef;
                    return;
                case EquipmentType.JEWELRY:
                    return;
            }
        }

        public void ExecuteAction(Vector3 point) { }
        public void ExecuteAction(Transform target)
        {
            if (!target.GetComponent<CombatEntity>().isDead)
            {
                this.combatTarget = target;
                transform.LookAt(target);
                if (!CanAttack(target))
                {
                    agent.destination = target.position;
                    animator.SetBool("attack", false);
                }
                else
                    animator.SetBool("attack", true);
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
            //print(Time.unscaledTime + "s: " + gameObject.name + " recv: " + telegram.ToString());
            //return GetComponent<FSM.FiniteStateMachine>().HandleMessage(telegram);

            return true;
        }
    }
}
