using UnityEngine;
using App.Manager;
using App.Control;
using App.SO;
using App.UI;
using App.Data;

namespace App.Items
{
    public class Potion : Item
    {
        protected override void Awake()
        {
            base.Awake();
            level = itemData.level = itemConfig.itemLevel;
        }
        
        void Start()
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
            rigidbody.constraints = containerType == ContainerType.WORLD ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
            collider.enabled = containerType == ContainerType.WORLD ? true : false;
            tag = containerType == ContainerType.WORLD ? "DropItem" : "Untagged";    
        }

        protected override void Update()
        {
            base.Update();
            if(cdTimer < itemConfig.cd)
                cdTimer = Mathf.Min(cdTimer + Time.deltaTime, itemConfig.cd);
        }

        public override void LoadToContainer(ItemData itemData)
        {
            switch (itemData.containerType)
            {
                case ContainerType.WORLD:
                    break;
                case ContainerType.BAG:
                    ItemSlot tempSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
                    Item item = Instantiate(itemConfig.item, InventoryManager.Instance.bag);
                    item.level = level;
                    InventoryManager.Instance.Add(item, Instantiate(itemConfig.itemUI, tempSlot.icons.transform));
                    tempSlot.count.text = tempSlot.count.text == "" ? "1" : (int.Parse(tempSlot.count.text) + 1).ToString();
                    break;
                case ContainerType.EQUIPMENT:
                    break;
                case ContainerType.ACTION:
                    break;
            }
        }

        public override void AddToInventory()
        {
            for (int i = 0; i < InventoryManager.Instance.ongoingQuests.Count; i++)
            {
                Item temp = InventoryManager.Instance.ongoingQuests[i].Target.GetComponent<Item>();
                if (temp != null && Equals(temp))
                    InventoryManager.Instance.ongoingQuests[i].UpdateProgress(1);
            }
            ItemSlot tempSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
            InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.bag), Instantiate(itemConfig.itemUI, tempSlot.icons.transform));
            tempSlot.count.text = tempSlot.count.text == "" ? "1" : (int.Parse(tempSlot.count.text) + 1).ToString();
        }

        public override void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
            for (int i = 0; i < InventoryManager.Instance.ongoingQuests.Count; i++)
            {
                Item temp = InventoryManager.Instance.ongoingQuests[i].Target.GetComponent<Item>();
                if (temp != null && Equals(temp))
                    InventoryManager.Instance.ongoingQuests[i].UpdateProgress(-1);
            }
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
            itemSlot.count.text = itemSlot.count.text == "1" ? "" : (int.Parse(itemSlot.count.text) - 1).ToString();
            itemSlot.itemUI = itemSlot.icons.childCount > 0 ? itemSlot.icons.GetChild(0).GetComponent<ItemUI>() : null;
        }

        public override void Use(Entity user)
        {
            if (cdTimer < itemConfig.cd)
                UIManager.Instance.messagePanel.Print("冷却时间未到", Color.red);
            else
            {
                collider.enabled = true;
                PotionConfig potionConfig = itemConfig as PotionConfig;
                user.currentATK += potionConfig.atk;
                user.currentDEF += potionConfig.def;
                user.currentHP = Mathf.Min(user.currentHP + potionConfig.hp, user.professionAttribute.hp);
                user.hpBar.UpdateBar(new Vector3(user.currentHP / user.professionAttribute.hp, 1, 1));
                RemoveFromInventory();
                for (int i = 0; i < itemSlot.transform.GetChild(0).childCount; i++)
                    itemSlot.transform.GetChild(0).GetChild(i).GetComponent<ItemUI>().item.cdTimer = 0;
            }
        }
    }
}