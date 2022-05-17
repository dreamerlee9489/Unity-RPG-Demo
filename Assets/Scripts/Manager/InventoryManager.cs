using System.Collections.Generic;
using UnityEngine;
using App.Item;

namespace App.Manager
{
	public class InventoryManager
	{
		static InventoryManager instance = new InventoryManager();
		public static InventoryManager Instance => instance;
		public List<GameItem> items = new List<GameItem>();

		public void Add(GameItem item)
		{
			GameManager.Instance.canvas.bagPanel.Add(item);
			item.GetComponent<Collider>().enabled = false;
			items.Add(item);
		}

		public void Remove(GameItem item)
		{
			
		}
	}
}
