using UnityEngine;
using App.Items;

namespace App.UI
{
    public enum SlotType { BAG, ACTION, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, HAND, WEAPON, PANTS }

    public class ItemSlot : MonoBehaviour
    {
        public SlotType slotType = SlotType.BAG;
        public ItemType itemType { get; set; }
        public ItemUI itemUI = null;

        void Awake()
        {
            ResetItemType();
        }

        void ResetItemType()
        {
            switch (slotType)
            {
                case SlotType.ACTION:
                case SlotType.BAG:
                    itemType = ItemType.NONE;
                    return;
                case SlotType.BREAST:
                    itemType = ItemType.BREAST;
                    return;
                case SlotType.BOOTS:
                    itemType = ItemType.BOOTS;
                    return;
                case SlotType.HAND:
                    itemType = ItemType.HAND;
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

        public void Open(ItemUI itemUI)
        {
            this.itemUI = itemUI;
            this.itemType = itemUI.item.config.itemType;
            itemUI.transform.SetParent(transform);
            itemUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        public void Close()
        {
            if(itemUI != null)
            {
                itemUI.transform.SetParent(null);
                itemUI = null;
            }
            ResetItemType();
        }
    }
}