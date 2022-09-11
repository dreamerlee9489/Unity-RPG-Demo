using System.Collections.Generic;
using System.Linq;
using Control.CMD;
using UnityEngine;
using UnityEngine.AI;
using SO;
using Manager;
using Items;
using UI;
using Data;

namespace Control
{
    public enum CampType
    {
        Blue,
        Red
    }

    [RequireComponent(typeof(CapsuleCollider), typeof(NavMeshAgent), typeof(AudioSource))]
    public class Entity : MonoBehaviour, ICmdReceiver, IMsgReceiver
    {
        private float _duration = 0, _timer = 0;
        private Vector3 _initPos;
        private Weapon _initialWeapon = null;
        private static MapManager _mapManager = null;
        private static readonly int attack = Animator.StringToHash("attack");
        private static readonly int death = Animator.StringToHash("death");
        private static readonly int moveSpeed = Animator.StringToHash("moveSpeed");
        private static readonly int pickup = Animator.StringToHash("pickup");
        private static readonly int skillA = Animator.StringToHash("skillA");
        private static readonly int skillB = Animator.StringToHash("skillB");
        private static readonly int skillC = Animator.StringToHash("skillC");
        private static readonly int skillD = Animator.StringToHash("skillD");
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
        public float sqrFleeRadius { get; set; }
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
        public ProfessionAttribute professionAttribute { get; set; }
        public CampType campType { get; set; }

        private void Awake()
        {
            GetComponent<Collider>().enabled = true;
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
            nameBar = transform.GetChild(1).GetComponent<NameBar>();
            agent.stoppingDistance = entityConfig.stopDistance;
            agent.speed = entityConfig.runSpeed * entityConfig.runFactor;
            speedRate = 1;
            _initPos = transform.position;
            sqrFleeRadius = Mathf.Pow(entityConfig.fleeRadius, 2);
            sqrViewRadius = Mathf.Pow(entityConfig.viewRadius, 2);
            sqrAttackRadius = Mathf.Pow(agent.stoppingDistance, 2);
            _initialWeapon = Instantiate(entityConfig.weapon, weaponPos);
            if (_mapManager == null)
                _mapManager = GameObject.FindObjectOfType<MapManager>();
            if (CompareTag("Enemy"))
                campType = CampType.Red;
        }

        private void Start()
        {
            if (CompareTag("Player"))
            {
                hpBar = UIManager.Instance.hudPanel.hpBar;
                PlayerData playerData =
                    BinaryManager.Instance.LoadData<PlayerData>(InventoryManager.Instance.playerData.nickName +
                                                                "_PlayerData");
                if (playerData == null)
                {
                    level = 1;
                    currentEXP = 0;
                    professionAttribute = professionConfig.GetProfessionAttribute(level);
                    maxEXP = professionAttribute.exp;
                    currentHP = maxHP = professionAttribute.hp;
                    currentMP = maxMP = professionAttribute.mp;
                    AttachEquipment(currentWeapon = _initialWeapon);
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
                AttachEquipment(currentWeapon = _initialWeapon);
                if (_mapManager.mapData.mapEnemyDatas.ContainsKey(name))
                {
                    EnemyData entityData = _mapManager.mapData.mapEnemyDatas[name];
                    currentHP = entityData.currentHP;
                    currentMP = entityData.currentMP;
                    gameObject.SetActive(false);
                    transform.position =
                        new Vector3(entityData.position.x, entityData.position.y, entityData.position.z);
                    gameObject.SetActive(true);
                }
                else
                {
                    EnemyData entityData = new EnemyData
                    {
                        currentHP = currentHP = maxHP,
                        currentMP = currentMP = maxMP,
                        position = new Vector(transform.position)
                    };
                    _mapManager.mapData.mapEnemyDatas.Add(name, entityData);
                }
            }

            if (currentHP <= 0)
            {
                isDead = true;
                target = null;
                animator.SetBool(attack, false);
                animator.SetBool(death, true);
                agent.isStopped = true;
                agent.radius = 0;
                GetComponent<Collider>().enabled = false;
            }
        }

        private void Update()
        {
            if (speedRate != 1)
            {
                if (_timer < _duration)
                    _timer += Time.deltaTime;
                else
                {
                    speedRate = 1;
                    _timer = _duration = 0;
                    agent.speed = entityConfig.runSpeed * entityConfig.runFactor;
                    UIManager.Instance.messagePanel.Print(entityConfig.nickName + "的速度恢复正常。", Color.green);
                }
            }

            animator.SetFloat(moveSpeed, transform.InverseTransformVector(agent.velocity).z);
        }

        private void Attack() => TakeDamage(target);

        private void TakeDamage(Transform target, float factor = 1)
        {
            if (target != null)
            {
                Entity defender = target.GetComponent<Entity>();
                int crit = Random.Range(0f, 1f) < 0.2f ? 2 : 1;
                float damage = Mathf.Max((currentATK * factor - defender.currentDEF) * crit, 1);
                defender.currentHP = Mathf.Max(defender.currentHP - damage, 0);
                defender.hpBar.UpdateBar(new Vector3(defender.currentHP / defender.maxHP, 1, 1));
                defender.nameBar.damage.text = string.Format("{0:0}", damage);
                defender.nameBar.damage.GetComponent<Animation>().Play();
                if (crit != 1)
                {
                    defender.audioSource.clip =
                        Resources.LoadAsync("Audio/SFX_Take Damage Ouch " + Random.Range(1, 6)).asset as AudioClip;
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

        private void Death()
        {
            isDead = true;
            target = null;
            GetComponent<Collider>().enabled = false;
            audioSource.clip = Resources.LoadAsync("Audio/Death/death" + Random.Range(1, 6)).asset as AudioClip;
            audioSource.Play();
            animator.SetBool(attack, false);
            animator.SetBool(death, true);
            agent.isStopped = true;
            agent.radius = 0;
        }

        private void Drop()
        {
            List<Item> dropItems =
                dropListConfig.GetDropItems(professionAttribute, ref InventoryManager.Instance.playerData.golds);
            UIManager.Instance.goldPanel.UpdatePanel();
            foreach (var item in dropItems.Select(dropItem => Instantiate(dropItem,
                         transform.position + Vector3.up * 2 + UnityEngine.Random.insideUnitSphere,
                         Quaternion.Euler(90, 90, 90))))
            {
                _mapManager.mapData.mapItemDatas.Add(item.itemData);
            }

            if (CompareTag("Enemy"))
            {
                for (int i = 0; i < InventoryManager.Instance.ongoingQuests.Count; i++)
                {
                    Entity target = InventoryManager.Instance.ongoingQuests[i].Target.GetComponent<Entity>();
                    if (target != null && target.entityConfig.nickName == entityConfig.nickName)
                        InventoryManager.Instance.ongoingQuests[i].UpdateProgress(1);
                }

                GameManager.Instance.player.GetExprience(professionAttribute.exp * 0.5f);
            }
        }

        private void Pickup()
        {
            if (target != null)
            {
                _mapManager.mapData.mapItemDatas.Remove(target.GetComponent<Item>().itemData);
                target.GetComponent<Item>().AddToInventory();
                if (target.GetComponent<Item>().nameBar != null)
                {
                    Destroy(target.GetComponent<Item>().nameBar.gameObject);
                    target.GetComponent<Item>().nameBar = null;
                }

                UIManager.Instance.messagePanel.Print(
                    "[系统]  你拾取了" + target.GetComponent<Item>().itemConfig.itemName + " * 1", Color.green);
                animator.SetBool(pickup, false);
                Destroy(target.GetComponent<Item>().gameObject);
                target = null;
            }
        }

        private Vector3 GetHidePosition(NavMeshObstacle obstacle, NavMeshAgent target, float distanceFromBoundary = 3f)
        {
            float distAway = obstacle.radius + distanceFromBoundary;
            Vector3 toObstacle = (obstacle.transform.position - target.transform.position).normalized;
            return obstacle.transform.position + toObstacle * distAway;
        }

        public void ExecuteAction(Vector3 point)
        {
            agent.speed = entityConfig.runSpeed * entityConfig.runFactor * speedRate;
            agent.stoppingDistance = entityConfig.stopDistance;
            agent.destination = point;
        }

        public void ExecuteAction(Transform target)
        {
            this.target = target;
            if (target.GetComponent<Entity>() != null)
            {
                if (CanAttack(target))
                {
                    transform.LookAt(target);
                    animator.SetBool(attack, true);
                }
                else
                {
                    agent.destination = target.position;
                    animator.SetBool(attack, false);
                }
            }
            else
            {
                if (CanDialogue(target))
                {
                    transform.LookAt(target);
                    animator.SetBool(pickup, true);
                }
                else
                {
                    agent.destination = target.position;
                    animator.SetBool(pickup, false);
                }
            }
        }

        public void CancelAction()
        {
            target = null;
            animator.SetBool(attack, false);
            animator.SetBool(pickup, false);
            animator.ResetTrigger(skillA);
            animator.ResetTrigger(skillB);
            animator.ResetTrigger(skillC);
            animator.ResetTrigger(skillD);
            agent.stoppingDistance = entityConfig.stopDistance;
            agent.destination = transform.position + transform.forward;
        }

        public void AttachEquipment(Equipment equipment)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.Weapon:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currentATK = professionAttribute.atk + weaponConfig.atk;
                    currentDEF = professionAttribute.def;
                    equipment.transform.SetParent(weaponPos);
                    equipment.GetComponent<MeshRenderer>().enabled = true;
                    currentWeapon = equipment as Weapon;
                    animator.runtimeAnimatorController = weaponConfig.animatorController;
                    break;
                case EquipmentType.Armor:
                    break;
                case EquipmentType.Jewelry:
                    break;
            }

            equipment.containerType = ContainerType.Equipment;
        }

        public void DetachEquipment(Equipment equipment, ContainerType containerType = ContainerType.Bag)
        {
            switch (equipment.equipmentType)
            {
                case EquipmentType.Weapon:
                    WeaponConfig weaponConfig = equipment.itemConfig as WeaponConfig;
                    currentATK = professionAttribute.atk;
                    equipment.transform.SetParent(InventoryManager.Instance.bag);
                    equipment.GetComponent<MeshRenderer>().enabled = false;
                    currentWeapon = _initialWeapon;
                    animator.runtimeAnimatorController = (currentWeapon.itemConfig as WeaponConfig).animatorController;
                    break;
                case EquipmentType.Armor:
                    break;
                case EquipmentType.Jewelry:
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
                currentATK = professionAttribute.atk +
                             (currentWeapon == null ? 0 : (currentWeapon.itemConfig as WeaponConfig).atk);
            }

            UIManager.Instance.hudPanel.UpdatePanel();
            UIManager.Instance.attributePanel.UpdatePanel();
        }

        public bool HandleMessage(Telegram telegram)
        {
            print(Time.unscaledTime + "s: " + gameObject.name + " recv: " + telegram.ToString());
            return GetComponent<StateController>().HandleMessage(telegram);
        }

        public void SetMaxSpeed(float speedRate, float duration)
        {
            this.speedRate = speedRate;
            this._duration = duration;
            agent.speed = entityConfig.runSpeed * entityConfig.runFactor * speedRate;
            UIManager.Instance.messagePanel.Print(
                entityConfig.nickName + "的速度" +
                (speedRate > 1 ? "提升了" + (1 - speedRate * 100) : ("降低了" + speedRate * 100)) + "%。", Color.green);
        }

        public bool CanSee(Transform target)
        {
            if (!target.GetComponent<Entity>().isDead)
            {
                Vector3 direction = target.position - transform.position;
                if (direction.sqrMagnitude <= sqrViewRadius)
                    return true;
            }

            return false;
        }

        public bool CanAttack(Transform target)
        {
            if (!target.GetComponent<Entity>().isDead)
            {
                Vector3 direction = target.position - transform.position;
                if (direction.sqrMagnitude <= sqrAttackRadius)
                    return true;
            }

            return false;
        }

        public bool CanDialogue(Transform target)
        {
            Vector3 direction = target.position - transform.position;
            if (direction.sqrMagnitude <= 1f)
                return true;
            return false;
        }

        public float Seek(Vector3 position)
        {
            agent.autoBraking = false;
            agent.destination = position;
            return Vector3.Distance(position, agent.destination);
        }

        public bool Flee(Vector3 position)
        {
            Vector3 direction = transform.position - position;
            if (direction.sqrMagnitude <= Mathf.Pow(entityConfig.fleeRadius, 2))
            {
                agent.autoBraking = false;
                agent.destination = direction.normalized * entityConfig.fleeRadius;
            }

            return direction.sqrMagnitude > sqrFleeRadius;
        }

        public void Arrive(Vector3 position)
        {
            agent.autoBraking = true;
            agent.destination = position;
        }

        public void Pursuit(NavMeshAgent evader)
        {
            Vector3 toEvader = evader.transform.position - transform.position;
            float relativeHeading = Vector3.Dot(transform.forward, evader.transform.forward);
            if (Vector3.Dot(transform.forward, toEvader) > 0 && (relativeHeading < -0.95f))
            {
                Seek(evader.transform.position);
                return;
            }

            float lookAheadTime = toEvader.magnitude / (agent.speed + evader.speed);
            Seek(evader.transform.position + evader.velocity * lookAheadTime);
        }

        public void Evade(NavMeshAgent pursuer)
        {
            Vector3 toPursuer = pursuer.transform.position - transform.position;
            float lookAheadTime = toPursuer.magnitude / (agent.speed + pursuer.speed);
            Flee(pursuer.transform.position + pursuer.velocity * lookAheadTime);
        }

        public void Wander(float radius = 10f)
        {
            Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
            position = position.normalized * radius * Random.Range(0f, 1f);
            agent.destination = _initPos + position;
        }

        public void Interpose(NavMeshAgent agentA, NavMeshAgent agentB)
        {
            Vector3 midPoint = (agentA.transform.position + agentB.transform.position) / 2.0f;
            float timeToReachMidPoint = Vector3.Distance(transform.position, midPoint) / agent.speed;
            Vector3 posA = agentA.transform.position + agentA.velocity * timeToReachMidPoint;
            Vector3 posB = agentB.transform.position + agentB.velocity * timeToReachMidPoint;
            midPoint = (posA + posB) / 2.0f;
            Arrive(midPoint);
        }

        public void Hide(NavMeshAgent hunter, List<NavMeshObstacle> obstacles)
        {
            float distToClosest = float.MaxValue;
            Vector3 bestHideSpot = Vector3.zero;
            NavMeshObstacle closest = null;
            foreach (NavMeshObstacle obstacle in obstacles)
            {
                Vector3 hideSpot = GetHidePosition(obstacle, hunter);
                float dist = Vector3.Distance(hideSpot, transform.position);
                if (dist < distToClosest)
                {
                    distToClosest = dist;
                    bestHideSpot = hideSpot;
                    closest = obstacle;
                }
            }

            if (distToClosest == float.MaxValue)
            {
                Evade(hunter);
                return;
            }

            Arrive(bestHideSpot);
        }

        public void OffsetPursuit(NavMeshAgent leader, Vector3 offset)
        {
            Vector3 worldOffsetPos = leader.transform.TransformVector(offset);
            Vector3 toOffset = worldOffsetPos - transform.position;
            float lookAheadTime = toOffset.magnitude / (agent.speed + leader.speed);
            Arrive(worldOffsetPos + leader.velocity * lookAheadTime);
        }
    }
}