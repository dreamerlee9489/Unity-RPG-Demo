using UnityEngine;
using App.Control;
using App.Control.FSM;
using App.Manager;
using App.SO;

namespace App.Items
{
    public class Skill : Item
    {
        CombatEntity user = null;
        SkillConfig skillConfig = null;
        SkillAttribute skillAttribute = null;
        WeaponType professWeaponType = WeaponType.NONE;
        WeaponType currentWeaponType = WeaponType.NONE;

        protected override void Awake()
        {
            base.Awake();
            collider.enabled = false;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        protected override void Update()
        {
            if (cdTimer > 0)
            {
                cdTimer = Mathf.Max(cdTimer - Time.deltaTime, 0);
                if (cdTimer < skillAttribute.cd - skillAttribute.controlTime)
                {
                    user.currentHP = Mathf.Max(user.currentHP - skillAttribute.hp, 0);
                    user.currentMP = Mathf.Max(user.currentMP - skillAttribute.mp, 0);
                    user.currentATK -= skillAttribute.atk;
                    user.currentDEF -= skillAttribute.def;
                }
            }
            else
            {
                gameObject.SetActive(false);
                collider.enabled = false;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            CombatEntity target = other.GetComponent<CombatEntity>();
            if (target != null && target.campType != user.campType)
            {
                collider.enabled = false;
                switch (skillConfig.controlType)
                {
                    case ControlType.NONE:
                        break;
                    case ControlType.KNOCK:
                        target.GetComponent<FiniteStateMachine>().ChangeState(new Knocked(target, user));
                        UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "被击倒", Color.green);
                        break;
                    case ControlType.SPEED:
                        target.SetMaxSpeed(skillAttribute.controlRate, skillAttribute.controlTime);
                        UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "的速度" + (skillAttribute.controlRate > 1 ? "提升了" + (1 - skillAttribute.controlRate * 100) : ("降低了" + skillAttribute.controlRate * 100)) + "%", Color.green);
                        break;
                    case ControlType.STUNN:
                        target.GetComponent<FiniteStateMachine>().ChangeState(new Stunned(target, user, skillAttribute.controlTime));
                        UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "被眩晕", Color.green);
                        break;
                }
            }
        }

        public override void LoadToContainer(int level, ContainerType containerType)
        {
            switch (containerType)
            {
                case ContainerType.WORLD:
                    break;
                case ContainerType.BAG:
                    break;
                case ContainerType.EQUIPMENT:
                    break;
                case ContainerType.ACTION:
                    break;
                case ContainerType.SKILL:
                    Skill skill = Instantiate(itemConfig.item, InventoryManager.Instance.skills).GetComponent<Skill>();
                    skill.level = level;
                    InventoryManager.Instance.Add(skill, Instantiate(itemConfig.itemUI, UIManager.Instance.actionPanel.GetFirstValidSlot().icons.transform), ContainerType.SKILL);
                    break;
            }
        }

        public override void AddToInventory()
        {
            int index = InventoryManager.Instance.HasSkill(this);
            if (index != -1)
                InventoryManager.Instance.skills.GetChild(index).GetComponent<Skill>().level++;
            else
            {
                Skill skill = Instantiate(itemConfig.item, InventoryManager.Instance.skills).GetComponent<Skill>();
                skill.level = 1;
                InventoryManager.Instance.Add(skill, Instantiate(itemConfig.itemUI, UIManager.Instance.actionPanel.GetFirstValidSlot().icons.transform), ContainerType.SKILL);
            }
        }

        public override void RemoveFromInventory()
        {
        }

        public override void Use(CombatEntity user)
        {
            if (cdTimer > 0)
                UIManager.Instance.messagePanel.Print("冷却时间未到", Color.red);
            else
            {
                skillConfig = itemConfig as SkillConfig;
                skillAttribute = skillConfig.GetSkillAttribute(level);
                professWeaponType = user.professionConfig.weaponType;
                currentWeaponType = (user.currentWeapon.itemConfig as WeaponConfig).weaponType;
                if (professWeaponType == currentWeaponType)
                {
                    if (level > 0)
                    {
                        this.user = user;
                        user.currentHP = Mathf.Min(user.currentHP + skillAttribute.hp, user.maxHP);
                        user.currentMP = Mathf.Min(user.currentMP + skillAttribute.mp, user.maxMP);
                        user.currentATK += skillAttribute.atk;
                        user.currentDEF += skillAttribute.def;
                        cdTimer = itemConfig.cd;
                        gameObject.SetActive(true);
                        collider.enabled = true;
                        switch (skillConfig.skillType)
                        {
                            case SkillType.A:
                                user.animator.SetBool("attack", false);
                                user.animator.SetTrigger("skillA");
                                break;
                            case SkillType.B:
                                user.animator.SetBool("attack", false);
                                user.animator.SetTrigger("skillB");
                                break;
                            case SkillType.C:
                                user.animator.SetBool("attack", false);
                                user.animator.SetTrigger("skillC");
                                break;
                            case SkillType.D:
                                user.animator.SetBool("attack", false);
                                user.animator.SetTrigger("skillD");
                                break;
                        }
                    }
                    else
                    {
                        UIManager.Instance.messagePanel.Print("[系统]  你尚未学习该技能", Color.red);
                    }
                }
                else
                {
                    UIManager.Instance.messagePanel.Print("[系统]  武器类型不匹配，无法施展技能", Color.red);
                }
            }
        }
    }
}
