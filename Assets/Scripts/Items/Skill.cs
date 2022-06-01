using UnityEngine;
using App.Control;
using App.Control.FSM;
using App.Manager;
using App.SO;
using App.Data;

namespace App.Items
{
    public class Skill : Item
    {
        Entity user = null;
        SkillConfig skillConfig = null;
        SkillAttribute skillAttribute = null;
        WeaponType professWeaponType = WeaponType.NONE;
        WeaponType currentWeaponType = WeaponType.NONE;

        protected override void Awake()
        {
            base.Awake();
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        protected override void Update()
        {
            if(cdTimer < skillConfig.cd)
            {
                cdTimer = Mathf.Min(cdTimer + Time.deltaTime, skillConfig.cd);
            }
            else
            {
                collider.enabled = false;
                gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            Entity target = other.GetComponent<Entity>();
            if (target != null && target.campType != user.campType)
            {
                collider.enabled = false;
                switch (skillConfig.controlType)
                {
                    case ControlType.NONE:
                        break;
                    case ControlType.KNOCK:
                        target.GetComponent<StateController>()?.ChangeState(new Knocked(target, user));
                        break;
                    case ControlType.SPEED:
                        target.SetMaxSpeed(skillAttribute.controlRate, skillAttribute.controlTime);
                        break;
                    case ControlType.STUNN:
                        target.GetComponent<StateController>()?.ChangeState(new Stunned(target, user, skillAttribute.controlTime));
                        break;
                }
            }
        }

        public override void LoadToContainer(ItemData itemData)
        {
            switch (itemData.containerType)
            {
                case ContainerType.WORLD:
                    break;
                case ContainerType.BAG:
                    break;
                case ContainerType.EQUIPMENT:
                    break;
                case ContainerType.ACTION:
                    Skill skill = Instantiate(itemConfig.item, InventoryManager.Instance.skills).GetComponent<Skill>();
                    skill.level = level;
                    InventoryManager.Instance.Add(skill, Instantiate(itemConfig.itemUI, UIManager.Instance.actionPanel.GetFirstValidSlot().icons.transform), ContainerType.ACTION);
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
                InventoryManager.Instance.Add(skill, Instantiate(itemConfig.itemUI, UIManager.Instance.actionPanel.GetFirstValidSlot().icons.transform), ContainerType.ACTION);
            }
        }

        public override void RemoveFromInventory()
        {
        }

        public override void Use(Entity user)
        {
            if (cdTimer < itemConfig.cd)
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
                        cdTimer = 0;
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
