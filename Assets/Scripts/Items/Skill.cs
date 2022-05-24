using UnityEngine;
using App.Control;
using App.Manager;
using App.SO;

namespace App.Items
{
    public class Skill : Item
    {
        CombatEntity user = null;
        CombatEntity target = null;
        SkillConfig skillConfig = null;
        SkillAttribute skillAttribute = null;
        WeaponType professWeaponType = WeaponType.NONE;
        WeaponType currentWeaponType = WeaponType.NONE;
        float controlTimer = 0;
        public int level = 0;

        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(containerType == ContainerType.BAG ? false : true);
        }

        void OnEnable()
        {
            collider.enabled = true;
            controlTimer = skillAttribute.controlTime;
            user.currentHP = Mathf.Min(user.currentHP + skillAttribute.hp, user.maxHP);
            user.currentMP = Mathf.Min(user.currentMP + skillAttribute.mp, user.maxMP);
            user.currentATK += skillAttribute.atk;
            user.currentDEF += skillAttribute.def;
            UIManager.Instance.attributePanel.UpdatePanel();
        }

        protected override void Update()
        {
            if (target != null)
            {
                if (controlTimer < skillAttribute.controlTime)
                    controlTimer = Mathf.Min(controlTimer + Time.deltaTime, skillAttribute.controlTime);
                if (controlTimer == skillAttribute.controlTime)
                {
                    switch (skillConfig.controlType)
                    {
                        case ControlType.NONE:
                            break;
                        case ControlType.KNOCK:
                            target.agent.isStopped = false;
                            target.immovable = false;
                            UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "站起来了。", Color.green);
                            break;
                        case ControlType.SPEED:
                            target.SetMaxSpeed(1);
                            UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "的速度恢复正常。", Color.green);
                            break;
                        case ControlType.STUNN:
                            target.animator.SetBool("stunned", false);
                            target.agent.isStopped = false;
                            target.immovable = false;
                            UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "解除了眩晕。", Color.green);
                            break;
                    }
                    gameObject.SetActive(false);
                    target = null;
                    user.currentSkill = null;    
                    user.currentHP = Mathf.Max(user.currentHP - skillAttribute.hp, 0);
                    user.currentMP = Mathf.Max(user.currentMP - skillAttribute.mp, 0);
                    user.currentATK -= skillAttribute.atk;
                    user.currentDEF -= skillAttribute.def;
                    UIManager.Instance.attributePanel.UpdatePanel();
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            CombatEntity temp = other.GetComponent<CombatEntity>();
            if (temp != null && temp.campType != user.campType)
            {
                target = temp;
                controlTimer = 0;
                collider.enabled = false;
                switch (skillConfig.controlType)
                {
                    case ControlType.NONE:
                        break;
                    case ControlType.KNOCK:
                        target.animator.SetTrigger("knock");
                        target.agent.isStopped = true;
                        target.immovable = true;
                        target.animator.SetBool("attack", false);
                        UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "被击倒", Color.green);
                        break;
                    case ControlType.SPEED:
                        target.SetMaxSpeed(skillAttribute.controlRate);
                        UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "的速度" + (skillAttribute.controlRate > 1 ? "提升了" + (1 - skillAttribute.controlRate * 100) : ("降低了" + skillAttribute.controlRate * 100)) + "%", Color.green);
                        break;
                    case ControlType.STUNN:
                        target.animator.SetBool("stunned", true);
                        target.agent.isStopped = true;
                        target.immovable = true;
                        target.animator.SetBool("attack", false);
                        UIManager.Instance.messagePanel.Print(target.entityConfig.nickName + "被眩晕", Color.green);
                        break;
                }
            }
        }

        public override void AddToInventory()
        {
            int index = InventoryManager.Instance.HasSkill(this);
            if (index != -1)
                InventoryManager.Instance.skills.GetChild(index).GetComponent<Skill>().level++;
            else
            {
                Skill skill = Instantiate(itemConfig.itemPrefab, InventoryManager.Instance.skills).GetComponent<Skill>();
                skill.level = 1;
                InventoryManager.Instance.Add(skill, Instantiate(itemConfig.itemUI, UIManager.Instance.actionPanel.GetFirstValidSlot().icons.transform));
            }
        }

        public override void RemoveFromInventory() { }

        public override void Use(CombatEntity user)
        {
            if (cdTimer < itemConfig.cd)
                UIManager.Instance.messagePanel.Print("冷却时间未到", Color.red);
            else
            {
                skillConfig = itemConfig as SkillConfig;
                skillAttribute = skillConfig.GetSkillAttributeByLevel(level);
                professWeaponType = user.professionConfig.weaponType;
                currentWeaponType = (user.currentWeapon.itemConfig as WeaponConfig).weaponType;
                if (professWeaponType == currentWeaponType)
                {
                    if (level > 0)
                    {
                        cdTimer = 0;
                        this.user = user;
                        user.currentSkill = this;
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
