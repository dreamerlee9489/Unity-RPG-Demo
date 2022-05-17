using UnityEngine;
using App.Item;

namespace App.UI
{
    public enum PanelType { NONE, BAG, EQUIPMENT, ACTION }
    public enum SlotType { BAG, HELMET, ARMOR, SHIELD, BOOTS, NECKLACE, BRACER, WEAPON, PANTS, ACTION }
    public enum ItemType { NONE, HELMET, ARMOR, SHIELD, BOOTS, NECKLACE, BRACER, WEAPON, PANTS, POTION, SKILL }

    public class ItemSlot : MonoBehaviour
    {
        public PanelType panelType = PanelType.BAG;
        public SlotType slotType = SlotType.BAG;
        [HideInInspector] public ItemType itemType = ItemType.NONE;
        public ItemUI itemUI = null;

        void Awake()
        {
            ResetItemType();
        }

        private void ResetItemType()
        {
            switch (slotType)
            {
                case SlotType.ACTION:
                case SlotType.BAG:
                    itemType = ItemType.NONE;
                    return;
                case SlotType.ARMOR:
                    itemType = ItemType.ARMOR;
                    return;
                case SlotType.BOOTS:
                    itemType = ItemType.BOOTS;
                    return;
                case SlotType.BRACER:
                    itemType = ItemType.BRACER;
                    return;
                case SlotType.HELMET:
                    itemType = ItemType.HELMET;
                    return;
                case SlotType.NECKLACE:
                    itemType = ItemType.NECKLACE;
                    return;
                case SlotType.PANTS:
                    itemType = ItemType.PANTS;
                    return;
                case SlotType.SHIELD:
                    itemType = ItemType.SHIELD;
                    return;
                case SlotType.WEAPON:
                    itemType = ItemType.WEAPON;
                    return;
            }
        }

        public void Add(GameItem item)
        {
            itemUI = Instantiate(Resources.Load<ItemUI>("UI/ItemUI/" + item.itemUI.name), transform);
            itemUI.item = item;
            itemType = item.itemType;
        }

        public void Remove()
        {
            if(transform.childCount != 0)
                Destroy(transform.GetChild(0).gameObject);
            ResetItemType();
        }
    }
}