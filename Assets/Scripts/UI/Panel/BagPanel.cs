using UnityEngine;

namespace App.UI
{
    public class BagPanel : BasePanel
    {
        public Transform content = null;

        public void Open(ItemUI itemUI)
        {
            GetFirstNullSlot().Open(itemUI);
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

        public ItemSlot GetFirstNullSlot()
        {
            for (int i = 0; i < content.childCount; i++)
            {
                if(content.GetChild(i).GetComponent<ItemSlot>().itemUI == null)
                    return content.GetChild(i).GetComponent<ItemSlot>();
            }
            return null;
        }
    }
}
