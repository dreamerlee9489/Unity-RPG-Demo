using UnityEngine;
using UnityEngine.UI;
using Items;

namespace UI
{
    public enum SlotType
    {
        BAG,
        ACTION,
        HELMET,
        BREAST,
        SHIELD,
        BOOTS,
        NECKLACE,
        HAND,
        WEAPON,
        PANTS
    }

    public class ItemSlot : MonoBehaviour
    {
        public SlotType slotType = SlotType.BAG;
        public ItemType itemType { get; set; }
        public ItemUI itemUI = null;
        public Transform icons { get; set; }
        public Text count { get; set; }

        void Awake()
        {
            icons = transform.GetChild(0);
            count = transform.GetChild(1).GetComponent<Text>();
            ResetItemType();
        }

        void ResetItemType()
        {
            switch (slotType)
            {
                case SlotType.ACTION:
                case SlotType.BAG:
                    itemType = ItemType.None;
                    return;
                case SlotType.BREAST:
                    itemType = ItemType.Breast;
                    return;
                case SlotType.BOOTS:
                    itemType = ItemType.Boots;
                    return;
                case SlotType.HAND:
                    itemType = ItemType.Bracelet;
                    return;
                case SlotType.HELMET:
                    itemType = ItemType.Helmet;
                    return;
                case SlotType.NECKLACE:
                    itemType = ItemType.Necklace;
                    return;
                case SlotType.PANTS:
                    itemType = ItemType.Pants;
                    return;
                case SlotType.SHIELD:
                    itemType = ItemType.Shield;
                    return;
                case SlotType.WEAPON:
                    itemType = ItemType.Weapon;
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
            if (itemUI != null)
            {
                itemUI.transform.SetParent(null);
                itemUI = null;
            }

            ResetItemType();
        }
    }
}