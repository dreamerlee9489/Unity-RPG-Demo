using System;
using App.Items;
using UnityEngine;

namespace App.UI
{
    public class BagPanel : BasePanel
    {
        public Transform content = null;

        public void Open(ItemUI itemUI)
        {
            GetFirstValidSlot().Open(itemUI);
        }

        public void Close(ItemUI itemUI)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                ItemSlot slot = content.GetChild(i).GetComponent<ItemSlot>();
                if(itemUI == slot.itemUI)
                {
                    slot.Close();
                    return;
                }
            }
        }

        public ItemSlot GetFirstValidSlot()
        {
            for (int i = 0; i < content.childCount; i++)
            {
                if(content.GetChild(i).GetComponent<ItemSlot>().itemUI == null)
                    return content.GetChild(i).GetComponent<ItemSlot>();
            }
            return null;
        }

        public ItemSlot GetStackSlot(GameItem item)
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
