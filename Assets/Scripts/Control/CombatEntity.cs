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
        float duration = 0, timer = 0;
        Item pickup = null;
        Weapon initialWeapon = null;
        static MapManager mapManager = null;
        public Transform weaponPos = null;
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
        public float speedRate { get; set; }
        public bool isDead { get; set; }
        public Animator animator { get; set; }
        public NavMeshAgent agent { get; set; }
        public Transform target { get; set; }
        public HUDBar hpBar { get; set; }
        public Weapon currentWeapon { get; set; }
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
            agent.speed = entityConfig.runSpeed * entityConfig.runFactor;
            sqrViewRadius = Mathf.Pow(entityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
            initialWeapon = Instantiate(entityConfig.weapon, weaponPos);
            speedRate = 1;
            if(mapManager == null)
                mapManager = GameObject.FindObjectOfType<MapManager>();
            if (CompareTag("Player"))
                campType = CampType.BLUE;
            else if (CompareTag("Enemy"))
                campType = CampType.RED;
            else
                campType = CampType.YELLOW;
        }

        void Start()
        {
            if(CompareTag("Player"))
            {
                hpBar = UIManager.Instance.hudPanel.hpBar;
                PlayerData playerData = JsonManager.Instance.LoadData<PlayerData>(InventoryManager.Instance.playerData.nickName + "_PlayerData");
                if(playerData == null)
                {
                    level = 1;
                    currentEXP = 0;
                    professionAttribute = professionConfig.GetProfessionAttribute(level);
                    maxEXP = professionAttribute.exp;
                    currentHP = maxHP = professionAttribute.hp;
                    currentMP = maxMP = professionAttribute.mp;
                    AttachEquipment(currentWeapon = initialWeapon);
                }
                else
                {
                    level = playerData.level;
                    currentEXP = playerData.currentEXP;
                    professionAttribute = professionConfig.GetProfessionAttribute(level);
                    maxEXP = professionAttribute.exp;
                    maxHP = professionAttribute.hp;
                    maxMP = professionAttribute.mp;
                    currentHP = playerData.currentHP;
                    currentMP = playerData.currentMP;
                }
            }
            else
            {
                hpBar = transform.GetChild(0).GetComponent<HUDBar>();
                professionAttribute = professionConfig.GetProfessionAttribute(level = entityConfig.level);
                maxHP = professionAttribute.hp;
                maxMP = professionAttribute.mp;
                AttachEquipment(currentWeapon = initialWeapon);
                if (mapManager.mapData.mapEntityDatas.ContainsKey(name))
                {
                    EntityData entityData = mapManager.mapData.mapEntityDatas[name];
                    currentHP = entityData.currentHP;
                    currentMP = entityData.currentMP;
                    gameObject.SetActive(false);
                    transform.position = new Vector3(entityData.position.x, entityData.position.y, entityData.position.z);
                    gameObject.SetActive(true);
                }
                else
                {
                    EntityData entityData = new EntityData();
                    entityData.currentHP = currentHP = maxHP;
                    entityData.currentMP = currentMP = maxMP;
                    entityData.position = new Vector(transform.position);
                    mapManager.mapData.mapEntityDatas.Add(name, entityData);
                }
            }
            if (currentHP <= 0)
                Death();
        }

        void Update()
        {
            if(speedRate != 1)
            {
                if(timer < duration)
                    timer += Time.deltaTime;
                else
                {
                    speedRate = 1;
                    timer = duration = 0;
                    agent.speed = entityConfig.runSpeed * entityConfig.runFactor;
                }
            }
        }

        void Attack() => TakeDamage(target);
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
                    defender.Drop();
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
            agent.isStopped = true;
            agent.radius = 0;
            GetComponent<Collider>().enabled = false;
        }

        void Drop()
        {
            List<Item> dropItems = dropListConfig.GetDropItems(professionAttribute, ref InventoryManager.Instance.playerData.golds);
            UIManager.Instance.goldPanel.UpdatePanel();
            foreach (var dropItem in dropItems)
            {
                Item item = Instantiate(dropItem, transform.position + Vector3.up * 2 + UnityEngine.Random.insideUnitSphere, Quaternion.Euler(90, 90, 90));
                mapManager.mapData.mapItemDatas.Add(item.itemData);
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
                mapManager.mapData.mapItemDatas.Remove(pickup.itemData);
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

        public void SaveEntityData()
        {
            EntityData entityData = mapManager.mapData.mapEntityDatas[name];
            entityData.currentHP = currentHP;
            entityData.currentMP = currentMP;
            entityData.position = new Vector(transform.position);
        }

        public void LoadEntityData()
        {
            EntityData entityData = mapManager.mapData.mapEntityDatas[name];
            currentHP = entityData.currentHP;
            currentMP = entityData.currentMP;
            transform.position = new Vector3(entityData.position.x, entityData.position.y, entityData.position.z);
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
                    currentDEF = professionAttribute.def;
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

        public bool HandleMessage(Telegram telegram)
        {
            print(Time.unscaledTime + "s: " + gameObject.name + " recv: " + telegram.ToString());
            return GetComponent<FSM.FiniteStateMachine>().HandleMessage(telegram);
        }

        public void SetMaxSpeed(float speedRate, float duration)
        {
            this.speedRate = speedRate;
            this.duration = duration;
            agent.speed = entityConfig.runSpeed * entityConfig.runFactor * speedRate;
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
                if (direction.sqrMagnitude <= sqrAttackRadius && Vector3.Dot(transform.forward, direction.normalized) > 0)
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
    }
}
