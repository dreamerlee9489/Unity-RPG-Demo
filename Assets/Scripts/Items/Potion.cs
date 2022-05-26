using UnityEngine;
using App.Manager;
using App.Control;
using App.SO;
using App.UI;

namespace App.Items
{
    public class Potion : Item
    {
        public override void LoadToContainer(int level, ContainerType containerType)
        {
            switch (containerType)
            {
                case ContainerType.WORLD:
                    break;
                case ContainerType.BAG:
                    ItemSlot tempSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
                    Item item = Instantiate(itemConfig.itemPrefab, InventoryManager.Instance.bag);
                    item.level = level;
                    InventoryManager.Instance.Add(item, Instantiate(itemConfig.itemUI, tempSlot.icons.transform));
                    tempSlot.count.text = tempSlot.count.text == "" ? "1" : (int.Parse(tempSlot.count.text) + 1).ToString();
                    break;
                case ContainerType.EQUIPMENT:
                    break;
                case ContainerType.ACTION:
                    break;
                case ContainerType.SKILL:
                    break;
            }
        }

        public override void AddToInventory()
        {
            ItemSlot tempSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
            InventoryManager.Instance.Add(Instantiate(itemConfig.itemPrefab, InventoryManager.Instance.bag), Instantiate(itemConfig.itemUI, tempSlot.icons.transform));
            tempSlot.count.text = tempSlot.count.text == "" ? "1" : (int.Parse(tempSlot.count.text) + 1).ToString();
        }

        public override void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
            for (int i = 0; i < GameManager.Instance.ongoingTasks.Count; i++)
            {
                Item temp = GameManager.Instance.ongoingTasks[i].target.GetComponent<Item>();
                if (temp != null && Equals(temp))
                    GameManager.Instance.ongoingTasks[i].UpdateProgress(-1);
            }
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
            itemSlot.count.text = itemSlot.count.text == "1" ? "" : (int.Parse(itemSlot.count.text) - 1).ToString();
            itemSlot.itemUI = itemSlot.icons.childCount > 0 ? itemSlot.icons.GetChild(0).GetComponent<ItemUI>() : null;
        }

        public override void Use(CombatEntity user)
        {
            if (cdTimer > 0)
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
                    itemSlot.transform.GetChild(0).GetChild(i).GetComponent<ItemUI>().item.cdTimer = itemConfig.cd;
            }
        }
    }
}