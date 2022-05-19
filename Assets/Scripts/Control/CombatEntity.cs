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
        public HealthBar healthBar = null;
        public float currHp = 0;
        public float currDef = 0;
        public float currAtk = 0;
        public float sqrViewRadius { get; set; }
        public float sqrAttackRadius { get; set; }
        public bool isDead { get; set; }
        public bool isQuestTarget { get; set; }
        public Transform target { get; set; }
        public AbilityConfig abilityConfig { get; set; }
        public DropListConfig dropListConfig = null;

        void Awake()
        {
            if (CompareTag("Enemy"))
                healthBar = transform.GetChild(0).GetComponent<HealthBar>();
            GetComponent<Collider>().isTrigger = true;
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            abilityConfig = GetComponent<MoveEntity>().abilityConfig;
            agent.stoppingDistance = abilityConfig.stopDistance;
            agent.radius = 0.5f;
            currHp = abilityConfig.hp;
            currDef = abilityConfig.def;
            currAtk = abilityConfig.atk + (weapon.config as WeaponConfig).atk;
            sqrViewRadius = Mathf.Pow(abilityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
        }

        void AttackL() => TakeDamage(target);
        void AttackR() => TakeDamage(target);

        void TakeDamage(Transform target, float atkFactor = 1)
        {
            if (target != null)
            {
                CombatEntity defender = target.GetComponent<CombatEntity>();
                defender.currHp = Mathf.Max(defender.currHp + defender.currDef - currAtk * atkFactor, 0);
                defender.healthBar.UpdateBar(new Vector3(defender.currHp / defender.abilityConfig.hp, 1, 1));
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
            animator.SetBool("attack", false);
            animator.SetBool("death", true);
            agent.radius = 0;
            GetComponent<Collider>().enabled = false;
            if (isQuestTarget)
            {
                for (int i = 0; i < GameManager.Instance.ongoingQuests.Count; i++)
                {
                    Quest quest = GameManager.Instance.ongoingQuests[i];
                    if (quest.target == GetComponent<MoveEntity>().nickName)
                        quest.UpdateProgress(1);
                }
            }
            List<GameItem> drops = dropListConfig.GetDrops(abilityConfig, ref InventoryManager.Instance.playerData.golds);
            UIManager.Instance.goldPanel.UpdatePanel();
            foreach (var item in drops)
            {
                Instantiate(item, transform.position + Vector3.up + Random.insideUnitSphere, Quaternion.Euler(90, 90, 90));
            }
        }

        public void AttachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.config as WeaponConfig;
                    currAtk = abilityConfig.atk + weaponConfig.atk;
                    weapon = equipment as Weapon;
                    animator.runtimeAnimatorController = weaponConfig.animatorController;
                    equipment.transform.SetParent(unarmedWeapon.transform);
                    equipment.gameObject.SetActive(true);
                    return;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.config as ArmorConfig;
                    currHp = abilityConfig.hp + armorConfig.hp;
                    currDef = abilityConfig.def + armorConfig.def;
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
                    WeaponConfig weaponConfig = equipment.config as WeaponConfig;
                    currAtk = abilityConfig.atk;
                    weapon = unarmedWeapon;
                    animator.runtimeAnimatorController = Resources.LoadAsync("Animator/Unarmed Controller").asset as RuntimeAnimatorController;
                    equipment.transform.SetParent(InventoryManager.Instance.inventory);
                    equipment.gameObject.SetActive(false);
                    return;
                case EquipmentType.ARMOR:
                    ArmorConfig armorConfig = equipment.config as ArmorConfig;
                    currHp = abilityConfig.hp;
                    currDef = abilityConfig.def;
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
                this.target = target;
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
            target = null;
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
