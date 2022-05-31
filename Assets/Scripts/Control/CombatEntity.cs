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
    public enum CampType { BLUE, RED }

    public class CombatEntity : MonoBehaviour, ICmdReceiver, IMsgReceiver
    {
        float duration = 0, timer = 0;
        Weapon initialWeapon = null;
        public Transform weaponPos = null;
        public EntityConfig entityConfig = null;
        public ProfessionConfig professionConfig = null;
        public DropListConfig dropListConfig = null;
        public static MapManager mapManager = null;
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
        public AudioSource audioSource { get; set; }
        public Transform target { get; set; }
        public HUDBar hpBar { get; set; }
        public NameBar nameBar { get; set; }
        public Weapon currentWeapon { get; set; }
        public CampType campType { get; set; }
        public ProfessionAttribute professionAttribute { get; set; }
        public EnemyData entityData { get; set; }

        void Awake()
        {
            GetComponent<Collider>().enabled = true;
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
            nameBar = transform.GetChild(1).GetComponent<NameBar>();
            audioSource.playOnAwake = false;
            agent.stoppingDistance = entityConfig.stopDistance;
            agent.radius = 0.3f;
            agent.speed = entityConfig.runSpeed * entityConfig.runFactor;
            speedRate = 1;
            sqrViewRadius = Mathf.Pow(entityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
            initialWeapon = Instantiate(entityConfig.weapon, weaponPos);
            if(mapManager == null)
                mapManager = GameObject.FindObjectOfType<MapManager>();
            if (CompareTag("Enemy"))
                campType = CampType.RED;
        }

        void Start()
        {
            if(CompareTag("Player"))
            {
                hpBar = UIManager.Instance.hudPanel.hpBar;
                PlayerData playerData = BinaryManager.Instance.LoadData<PlayerData>(InventoryManager.Instance.playerData.nickName + "_PlayerData");
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
                    InventoryManager.Instance.LoadData(playerData);
                }
            }
            else
            {
                hpBar = transform.GetChild(0).GetComponent<HUDBar>();
                professionAttribute = professionConfig.GetProfessionAttribute(level = entityConfig.level);
                maxHP = professionAttribute.hp;
                maxMP = professionAttribute.mp;
                AttachEquipment(currentWeapon = initialWeapon);
                if (mapManager.mapData.mapEnemyDatas.ContainsKey(name))
                {
                    EnemyData entityData = mapManager.mapData.mapEnemyDatas[name];
                    currentHP = entityData.currentHP;
                    currentMP = entityData.currentMP;
                    gameObject.SetActive(false);
                    transform.position = new Vector3(entityData.position.x, entityData.position.y, entityData.position.z);
                    gameObject.SetActive(true);
                }
                else
                {
                    EnemyData entityData = new EnemyData();
                    entityData.currentHP = currentHP = maxHP;
                    entityData.currentMP = currentMP = maxMP;
                    entityData.position = new Vector(transform.position);
                    mapManager.mapData.mapEnemyDatas.Add(name, entityData);
                }
            }
            if (currentHP <= 0)
            {
                isDead = true;
                target = null;
                animator.SetBool("attack", false);
                animator.SetBool("death", true);
                agent.isStopped = true;
                agent.radius = 0;
                GetComponent<Collider>().enabled = false;
            }
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
                    UIManager.Instance.messagePanel.Print(GetComponent<CombatEntity>().entityConfig.nickName + "的速度恢复正常。", Color.green);
                }
            }
        }

        void Attack() => TakeDamage(target);
        void TakeDamage(Transform target, float factor = 1)
        {
            if (target != null)
            {
                CombatEntity defender = target.GetComponent<CombatEntity>();
                int crit = Random.Range(0f, 1f) < 0.2f ? 2 : 1;
                float damage = Mathf.Max((currentATK * factor - defender.currentDEF) * crit, 1);
                defender.currentHP = Mathf.Max(defender.currentHP - damage, 0);
                defender.hpBar.UpdateBar(new Vector3(defender.currentHP / defender.maxHP, 1, 1));
                defender.nameBar.damage.GetComponent<Animation>().Stop();
                defender.nameBar.damage.text = damage.ToString();
                defender.nameBar.damage.GetComponent<Animation>().Play();
                if(crit != 1)
                {
                    defender.audioSource.clip = Resources.LoadAsync("Audio/SFX_Take Damage Ouch " + Random.Range(1, 6)).asset as AudioClip;
                    defender.audioSource.Play();
                }
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
            audioSource.clip = Resources.LoadAsync("Audio/Death/death" + Random.Range(1, 6)).asset as AudioClip;
            audioSource.Play();
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
                for (int i = 0; i < InventoryManager.Instance.ongoingTasks.Count; i++)
                {
                    CombatEntity target = InventoryManager.Instance.ongoingTasks[i].Target.GetComponent<CombatEntity>();
                    if (target != null && target.entityConfig.nickName == entityConfig.nickName)
                        InventoryManager.Instance.ongoingTasks[i].UpdateProgress(1);
                }
                GameManager.Instance.player.GetExprience(professionAttribute.exp * 0.5f);
            }
        }

        public void ExecuteAction(Vector3 point) { }
        public void ExecuteAction(Transform target)
        {
            this.target = target;
            if (CanAttack(target))
            {
                transform.LookAt(target);
                animator.SetBool("attack", true);
            }
            else
            {
                agent.destination = target.position;
                animator.SetBool("attack", false);
            }
        }

        public void CancelAction()
        {
            target = null;
            animator.SetBool("attack", false);
            animator.ResetTrigger("skillA");
            animator.ResetTrigger("skillB");
            animator.ResetTrigger("skillC");
            animator.ResetTrigger("skillD");
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
            UIManager.Instance.messagePanel.Print(entityConfig.nickName + "的速度" + (speedRate > 1 ? "提升了" + (1 - speedRate * 100) : ("降低了" + speedRate * 100)) + "%。", Color.green);
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
                if (direction.sqrMagnitude <= sqrAttackRadius)
                    return true;
            }
            return false;
        }
    }
}
