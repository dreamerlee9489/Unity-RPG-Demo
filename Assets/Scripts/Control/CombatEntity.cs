using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using App.SO;
using App.Manager;
using App.Items;
using App.UI;
using App.Data;

namespace App.Control
{
    public enum CampType { BLUE, YELLOW, RED }

    public class CombatEntity : MonoBehaviour, ICmdReceiver, IMsgReceiver
    {
        Item pickup = null;
        Weapon initialWeapon = null;
        public Transform weaponPos = null;
        public Weapon initialWeaponPrefab = null;
        public EntityConfig entityConfig = null;
        public ProfessionConfig professionConfig = null;
        public DropListConfig dropListConfig = null;
        public int level { get; set; }
        public float currentHP { get; set; }
        public float currentMP { get; set; }
        public float currentEXP { get; set; }
        public float currentDEF { get; set; }
        public float currentATK { get; set; }
        public float maxHP { get; set; }
        public float maxMP { get; set; }
        public float maxEXP { get; set; }
        public float sqrViewRadius { get; set; }
        public float sqrAttackRadius { get; set; }
        public float maxSpeed { get; set; }
        public bool isDead { get; set; }
        public bool immovable { get; set; }
        public Animator animator { get; set; }
        public NavMeshAgent agent { get; set; }
        public Transform target { get; set; }
        public HUDBar hpBar { get; set; }
        public Weapon currentWeapon { get; set; }
        public Skill currentSkill { get; set; }
        public CampType campType { get; set; }
        public ProfessionAttribute professionAttribute { get; set; }
        public EntityData entityData { get; set; }

        void Awake()
        {
            GetComponent<Collider>().enabled = true;
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = entityConfig.stopDistance;
            agent.radius = 0.5f;
            sqrViewRadius = Mathf.Pow(entityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
            maxSpeed = entityConfig.runSpeed * entityConfig.runFactor;
            agent.speed = maxSpeed;
            level = 1;
            professionAttribute = professionConfig.GetProfessionAttribute(level);
            maxHP = professionAttribute.hp;
            maxMP = professionAttribute.mp;
            maxEXP = professionAttribute.exp;
            AttachEquipment(currentWeapon = initialWeapon = Instantiate(initialWeaponPrefab, weaponPos));
            currentWeapon.collider.enabled = false;
            currentWeapon.rigidbody.useGravity = false;
            currentWeapon.rigidbody.isKinematic = true;
            if(CompareTag("Player"))
                campType = CampType.BLUE;
            else if (CompareTag("Enemy"))
                campType = CampType.RED;
            else
                campType = CampType.YELLOW;
        }

        void Start()
        {
            hpBar = CompareTag("Player") ? UIManager.Instance.hudPanel.hpBar : transform.GetChild(0).GetComponent<HUDBar>();
            if(MapManager.Instance.mapData.mapEntityDatas.ContainsKey(name))
            {
                gameObject.SetActive(false);
                currentHP = MapManager.Instance.mapData.mapEntityDatas[name].currentHP;
                currentMP = MapManager.Instance.mapData.mapEntityDatas[name].currentMP;
                currentATK = MapManager.Instance.mapData.mapEntityDatas[name].currentATK;
                currentDEF = MapManager.Instance.mapData.mapEntityDatas[name].currentDEF;
                currentEXP = MapManager.Instance.mapData.mapEntityDatas[name].currentEXP;
                transform.position = new Vector3(MapManager.Instance.mapData.mapEntityDatas[name].position.x, MapManager.Instance.mapData.mapEntityDatas[name].position.y, MapManager.Instance.mapData.mapEntityDatas[name].position.z);
                gameObject.SetActive(true);
            }
            else
            {
                currentHP = professionAttribute.hp;
                currentMP = professionAttribute.mp;
                currentATK = professionAttribute.atk + (initialWeaponPrefab.itemConfig as WeaponConfig).atk;
                currentDEF = professionAttribute.def;
                currentEXP = 0;
                entityData = new EntityData();
                entityData.position = new Vector(transform.position);
                entityData.currentHP = currentHP;
                entityData.currentMP = currentMP;
                MapManager.Instance.mapData.mapEntityDatas.Add(name, entityData);
            }
            if(currentHP == 0)
            {
                isDead = true;
                target = null;
                animator.SetBool("attack", false);
                animator.SetBool("death", true);
                agent.radius = 0;
                GetComponent<Collider>().enabled = false;
            }
            UIManager.Instance.hudPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
        }

        void OnDestroy()
        {
            EntityData temp = MapManager.Instance.mapData.mapEntityDatas[name];
            temp.currentHP = currentHP;
            temp.currentMP = currentMP;
            temp.currentATK = currentATK;
            temp.currentDEF = currentDEF;
            temp.currentEXP = currentEXP;
            temp.position = new Vector(transform.position);
        }

        void Attack() => TakeDamage(target);
        void TakeDamage(Transform target, float factor = 1)
        {
            if (target != null)
            {
                CombatEntity defender = target.GetComponent<CombatEntity>();
                defender.currentHP = Mathf.Max(defender.currentHP - Mathf.Max(currentATK * factor - defender.currentDEF, 1), 0);
                defender.hpBar.UpdateBar(new Vector3(defender.currentHP / defender.maxHP, 1, 1));
                if (currentSkill != null)
                    currentSkill.gameObject.SetActive(true);
                if (defender.currentHP == 0)
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
            List<Item> dropItems = dropListConfig.GetDrops(professionAttribute, ref InventoryManager.Instance.playerData.golds);
            UIManager.Instance.goldPanel.UpdatePanel();
            foreach (var dropItem in dropItems)
            {
                Item item = Instantiate(dropItem, transform.position + Vector3.up * 2 + UnityEngine.Random.insideUnitSphere, Quaternion.Euler(90, 90, 90));
                MapManager.Instance.mapData.mapItemDatas.Add(item.itemData);
            }
            if (CompareTag("Enemy"))
            {
                for (int i = 0; i < GameManager.Instance.ongoingTasks.Count; i++)
                {
                    CombatEntity entity = GameManager.Instance.ongoingTasks[i].target.GetComponent<CombatEntity>();
                    if (entity != null && entity.entityConfig.nickName == entityConfig.nickName)
                        GameManager.Instance.ongoingTasks[i].UpdateProgress(1);
                }
                GameManager.Instance.player.GetExprience(professionAttribute.exp * 0.5f);
            }
        }

        void Pickup()
        {
            if (pickup != null)
            {
                for (int i = 0; i < GameManager.Instance.ongoingTasks.Count; i++)
                {
                    Item temp = GameManager.Instance.ongoingTasks[i].target.GetComponent<Item>();
                    if (temp != null && pickup.Equals(temp))
                        GameManager.Instance.ongoingTasks[i].UpdateProgress(1);
                }
                MapManager.Instance.mapData.mapItemDatas.Remove(pickup.itemData);
                pickup.AddToInventory();
                if (pickup.nameBar != null)
                {
                    Destroy(pickup.nameBar.gameObject);
                    pickup.nameBar = null;
                }
                UIManager.Instance.messagePanel.Print("[系统]  你拾取了" + pickup.itemConfig.itemName + " * 1", Color.green);
                animator.SetBool("pickup", false);
                Destroy(pickup.gameObject);
            }
        }

        public void LoadSkillTree()
        {
            if (CompareTag("Player"))
            {
                for (int i = 0; i < professionConfig.skillTree.Count; i++)
                    professionConfig.skillTree[i].AddToInventory();
            }
        }

        public void UnloadSkillTree()
        {
            if (CompareTag("Player"))
            {
                for (int i = 0; i < professionConfig.skillTree.Count; i++)
                    professionConfig.skillTree[i].RemoveFromInventory();
            }
        }

        public void AttachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.WEAPON:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currentATK = professionAttribute.atk + weaponConfig.atk;
                    animator.runtimeAnimatorController = weaponConfig.animatorController;
                    equipment.transform.SetParent(weaponPos);
                    equipment.gameObject.SetActive(true);
                    currentWeapon = equipment as Weapon;
                    break;
                case EquipmentType.ARMOR:
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
                    currentATK = professionAttribute.atk;
                    animator.runtimeAnimatorController = Resources.LoadAsync("Animator/Unarmed Controller").asset as RuntimeAnimatorController;
                    equipment.transform.SetParent(InventoryManager.Instance.bag);
                    equipment.gameObject.SetActive(false);
                    currentWeapon = initialWeapon;
                    break;
                case EquipmentType.ARMOR:
                    break;
                case EquipmentType.JEWELRY:
                    break;
            }
            equipment.containerType = containerType;
        }

        public void GetExprience(float exp)
        {
            currentEXP += exp;
            if (currentEXP >= maxEXP)
            {
                currentEXP -= maxEXP;
                professionAttribute = professionConfig.GetProfessionAttribute(++level);
                maxHP = professionAttribute.hp;
                maxMP = professionAttribute.mp;
                maxEXP = professionAttribute.exp;
                currentHP += professionAttribute.hp * 0.2f;
                currentMP += professionAttribute.mp * 0.2f;
                currentDEF = professionAttribute.def;
                currentATK = professionAttribute.atk + (currentWeapon == null ? 0 : (currentWeapon.itemConfig as WeaponConfig).atk);
            }
            UIManager.Instance.hudPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
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
            animator.ResetTrigger("skillA");
            animator.ResetTrigger("skillB");
            animator.ResetTrigger("skillC");
            animator.ResetTrigger("skillD");
        }

        public float SetMaxSpeed(float factor) => maxSpeed = entityConfig.runSpeed * entityConfig.runFactor * factor;
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
