﻿using UnityEngine;
using App.SO;
using App.Manager;
using App.Control;
using App.UI;

namespace App.Items
{
    public class Potion : Item
    {
        public override void AddToInventory()
        {
            ItemSlot itemSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
            InventoryManager.Instance.Add(Instantiate(itemConfig.itemPrefab, InventoryManager.Instance.bag), Instantiate(itemConfig.itemUI, itemSlot.icons.transform));
            itemSlot.count.text = itemSlot.count.text == "" ? "1" : (int.Parse(itemSlot.count.text) + 1).ToString();
        }

        public override void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
            ItemSlot itemSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
            itemSlot.count.text = itemSlot.count.text == "1" ? "" : (int.Parse(itemSlot.count.text) - 1).ToString();
            for (int i = 0; i < GameManager.Instance.registeredTasks.Count; i++)
            {
                Item temp = GameManager.Instance.registeredTasks[i].target.GetComponent<Item>();
                if (temp != null && Equals(temp))
                    GameManager.Instance.registeredTasks[i].UpdateProgress(-1);
            }
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
        }

        public override void Use(CombatEntity user)
        {
            PotionConfig potionConfig = itemConfig as PotionConfig;
            user.currentATK += potionConfig.atk;
            user.currentDEF += potionConfig.def;
            user.currentHP = Mathf.Min(user.currentHP + potionConfig.hp, user.attribute.thisLevelHP);
            user.hpBar.UpdateBar(new Vector3(user.currentHP / user.attribute.thisLevelHP, 1, 1));
            RemoveFromInventory();
        }
    }
}