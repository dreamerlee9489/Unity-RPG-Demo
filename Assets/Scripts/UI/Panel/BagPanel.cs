using Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BagPanel : BasePanel
    {
        public Transform content { get; set; }

        void Awake()
        {
            content = gameObject.GetComponentInChildren<ScrollRect>().content;
        }

        public void Draw(ItemUI itemUI)
        {
            GetFirstValidSlot().Draw(itemUI);
        }

        public void Erase(ItemUI itemUI)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                ItemSlot slot = content.GetChild(i).GetComponent<ItemSlot>();
                if (itemUI == slot.itemUI)
                {
                    slot.Erase();
                    return;
                }
            }
        }

        public ItemSlot GetFirstValidSlot()
        {
            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).GetComponent<ItemSlot>().itemUI == null)
                    return content.GetChild(i).GetComponent<ItemSlot>();
            }

            return null;
        }

        public ItemSlot GetStackSlot(Item item)
        {
            ItemSlot itemSlot = null;
            for (int i = 0; i < content.childCount; i++)
            {
                itemSlot = content.GetChild(i).GetComponent<ItemSlot>();
                if (itemSlot.itemUI != null && itemSlot.itemUI.item.Equals(item))
                    return itemSlot;
            }

            return GetFirstValidSlot();
        }
    }
}