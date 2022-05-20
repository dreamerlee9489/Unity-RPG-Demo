using UnityEngine;
using App.Config;
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
            InventoryManager.Instance.Add(Instantiate(itemConfig.item, InventoryManager.Instance.inventory), Instantiate(itemConfig.itemUI, itemSlot.icons.transform));
            itemSlot.count.text = itemSlot.count.text == "" ? "1" : (int.Parse(itemSlot.count.text) + 1).ToString();
        }

        public override void Use(CombatEntity user)
        {
            PotionConfig potionConfig = itemConfig as PotionConfig;
            user.currAtk += potionConfig.atk;
            user.currDef += potionConfig.def;
            user.currHp = Mathf.Min(user.currHp + potionConfig.hp, user.progression.thisLevelHp);
            user.healthBar.UpdateBar(new Vector3(user.currHp / user.progression.thisLevelHp, 1, 1));
            InventoryManager.Instance.Remove(this);
            ItemSlot itemSlot = UIManager.Instance.bagPanel.GetStackSlot(this);
            itemSlot.count.text = itemSlot.count.text == "1" ? "" : (int.Parse(itemSlot.count.text) - 1).ToString();
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
        }
    }
}