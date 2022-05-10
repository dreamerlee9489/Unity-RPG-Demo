using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
	public class UIController : MonoBehaviour
	{
		public EquipmentPanel equipmentPanel = null;
		public InventoryPanel inventoryPanel = null;

		void Start()
		{
			equipmentPanel.Close();
			inventoryPanel.Close();
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.E))
				equipmentPanel.gameObject.SetActive(equipmentPanel.isOpened = !equipmentPanel.isOpened);
			if(Input.GetKeyDown(KeyCode.I))
				inventoryPanel.gameObject.SetActive(inventoryPanel.isOpened = !inventoryPanel.isOpened);
		}
	}
}
