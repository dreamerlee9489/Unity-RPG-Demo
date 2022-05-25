using System.Collections.Generic;
using UnityEngine;
using App.Items;
using App.Enviorment;
namespace App.UI
{
	public class ActionPanel : BasePanel
	{
        ItemUI itemUI = null;

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                itemUI = transform.GetChild(0).GetComponent<ItemSlot>().itemUI;
                itemUI?.item.Use(GameManager.Instance.player);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                itemUI = transform.GetChild(1).GetComponent<ItemSlot>().itemUI;
                itemUI?.item.Use(GameManager.Instance.player);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                itemUI = transform.GetChild(2).GetComponent<ItemSlot>().itemUI;
                itemUI?.item.Use(GameManager.Instance.player);
            }
            if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                itemUI = transform.GetChild(3).GetComponent<ItemSlot>().itemUI;
                itemUI?.item.Use(GameManager.Instance.player);
            }
            if(Input.GetKeyDown(KeyCode.Alpha5))
            {
                itemUI = transform.GetChild(4).GetComponent<ItemSlot>().itemUI;
                itemUI?.item.Use(GameManager.Instance.player);
            }
            if(Input.GetKeyDown(KeyCode.Alpha6))
            {
                itemUI = transform.GetChild(5).GetComponent<ItemSlot>().itemUI;
                itemUI?.item.Use(GameManager.Instance.player);
            }
        }
		
		public ItemSlot GetFirstValidSlot()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<ItemSlot>().itemUI == null)
                    return transform.GetChild(i).GetComponent<ItemSlot>();
            }
            return null;
        }
	}
}
