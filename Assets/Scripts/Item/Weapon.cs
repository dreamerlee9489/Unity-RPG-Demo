using UnityEngine;
using App.Manager;
using App.SO;
using App.Control;
using App.UI;

namespace App.Item
{
    public class Weapon : GameItem
    {
        public Weapon(ItemConfig itemConfig) : base(itemConfig) { itemType = ItemType.WEAPON; }

        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InventoryManager.Instance.Add(Resources.Load<Weapon>("Item/" + itemConfig.path));
                Destroy(gameObject);
            }
        }

        public override void Use(Transform user)
        {
            CombatEntity entity = user.GetComponent<CombatEntity>();
            WeaponConfig config = itemConfig as WeaponConfig;
            switch (panelType)
            {
                case PanelType.ACTION:
                case PanelType.BAG:
                    panelType = PanelType.EQUIPMENT;
                    entity.weapon = this;
                    entity.currAtk = entity.abilityConfig.atk + config.atk;
                    entity.GetComponent<Animator>().runtimeAnimatorController = config.animatorController;
                    GameManager.Instance.canvas.bagPanel.Remove(this);
                    GameManager.Instance.canvas.equipmentPanel.weaponSlot.Add(this);
                    Instantiate(this, entity.weaponPos);
                    break;
                case PanelType.EQUIPMENT:
                    panelType = PanelType.BAG;
                    entity.currAtk = entity.abilityConfig.atk;
                    // entity.weapon = Resources.Load<WeaponConfig>("Config/Weapon/WeaponConfig_Unarmed");
                    entity.weapon = null;
                    entity.GetComponent<Animator>().runtimeAnimatorController = Resources.LoadAsync("Animators/Unarmed Controller").asset as RuntimeAnimatorController;
                    GameManager.Instance.canvas.equipmentPanel.weaponSlot.Remove();
                    GameManager.Instance.canvas.bagPanel.Add(this);
                    Destroy(entity.weaponPos.GetChild(0).gameObject);
                    break;
            }
        }
    }
}