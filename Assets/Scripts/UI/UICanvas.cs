using UnityEngine;

namespace App.UI
{
	public class UICanvas : MonoBehaviour
	{
		public EquipmentPanel equipmentPanel = null;
		public InventoryPanel inventoryPanel = null;

		void Awake()
		{
			equipmentPanel.gameObject.SetActive(false);
			inventoryPanel.gameObject.SetActive(false);
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
