using UnityEngine;
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
            InventoryManager.Instance.Add(Instantiate(itemConfig.itemPrefab, InventoryManager.Instance.inventory), Instantiate(itemConfig.itemUI, itemSlot.icons.transform));
            itemSlot.count.text = itemSlot.count.text == "" ? "1" : (int.Parse(itemSlot.count.text) + 1).ToString();
        }

        public override void RemoveFromInventory()
        {
            InventoryManager.Instance.Remove(this);
            ItemSlot itemSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
            itemSlot.count.text = itemSlot.count.text == "1" ? "" : (int.Parse(itemSlot.count.text) - 1).ToString();
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
        }

        public override void Use(CombatEntity user)
        {
            PotionConfig potionConfig = itemConfig as PotionConfig;
            user.currentATK += potionConfig.atk;
            user.currentDEF += potionConfig.def;
            user.currentHP = Mathf.Min(user.currentHP + potionConfig.hp, user.progression.thisLevelHP);
            user.hpBar.UpdateBar(new Vector3(user.currentHP / user.progression.thisLevelHP, 1, 1));
            RemoveFromInventory();
        }
    }
}