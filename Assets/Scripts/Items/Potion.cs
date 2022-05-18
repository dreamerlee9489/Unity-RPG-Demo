using UnityEngine;
using App.Config;
using App.Manager;
using App.Control;

namespace App.Items
{
    public class Potion : GameItem
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Instantiate(config.item, GameManager.Instance.player.transform), Instantiate(config.itemUI, GameManager.Instance.canvas.bagPanel.GetFirstValidSlot().transform));
                Destroy(gameObject);
            }
        }

        public override void Use(CombatEntity user)
        {
            PotionConfig potionConfig = config as PotionConfig;
            user.currAtk += potionConfig.atk;
            user.currDef += potionConfig.def;
            user.currHp += potionConfig.hp;
            user.healthBar.UpdateBar(new Vector3(user.currHp / user.abilityConfig.hp, 1, 1));
            InventoryManager.Instance.Remove(this);
            Destroy(this.itemUI.gameObject);
            Destroy(this.gameObject);
        }
    }
}