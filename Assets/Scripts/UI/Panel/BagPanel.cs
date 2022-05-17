using System.Collections.Generic;
using UnityEngine;
using App.Manager;
using App.Item;

namespace App.UI
{
    public class BagPanel : BasePanel
    {
        public Transform content = null;
        public List<ItemUI> itemUIs = new List<ItemUI>();
        
        public void Add(GameItem item)
        {
            item.panelType = PanelType.BAG;
            ItemUI itemUI = Instantiate(Resources.Load<ItemUI>("UI/ItemUI/" + item.itemUI.name), content.GetChild(itemUIs.Count));
            itemUI.item = item;
            itemUIs.Add(itemUI);
        }

        public void Remove(GameItem item)
        {
            for (int i = 0; i < itemUIs.Count; i++)
            {
                if(itemUIs[i].item == item)
                {
                    Destroy(itemUIs[i].gameObject);
                    itemUIs.RemoveAt(i);
                }
            }
        }
    }
}
