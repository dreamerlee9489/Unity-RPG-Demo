using UnityEngine;
using UnityEngine.UI;
using App.Items;

namespace App.UI
{
    public enum SlotType { BAG, ACTION, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, HAND, WEAPON, PANTS }

    public class ItemSlot : MonoBehaviour
    {
        public SlotType slotType = SlotType.BAG;
        public ItemType itemType { get; set; }
        public ItemUI itemUI { get; set; }
        public Transform icons = null;
        public Text count = null;

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

        public void Draw(ItemUI itemUI)
        {
            this.itemUI = itemUI;
            this.itemType = itemUI.item.itemConfig.itemType;
            itemUI.transform.SetParent(icons);
            itemUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }

        public void Erase()
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