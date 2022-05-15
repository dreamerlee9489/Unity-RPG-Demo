using App.Control;
using UnityEngine;

namespace App.UI
{
	public class UICanvas : MonoBehaviour, ICmdReceiver
	{
		public EquipmentPanel equipmentPanel = null;
		public InventoryPanel inventoryPanel = null;
		public DialoguePanel dialoguePanel = null;

        public void ExecuteAction(RaycastHit hit)
        {
			NPCController npc = hit.transform.GetComponent<NPCController>();
			dialoguePanel.npcName.text = npc.gameObject.name;
			dialoguePanel.dialoguesConfig = npc.dialoguesConfig;
			dialoguePanel.gameObject.SetActive(true);
        }

        public void CancelAction()
        {
			dialoguePanel.gameObject.SetActive(false);
        }

        void Awake()
		{
			equipmentPanel.gameObject.SetActive(false);
			inventoryPanel.gameObject.SetActive(false);
			dialoguePanel.gameObject.SetActive(false);
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.B))
				inventoryPanel.gameObject.SetActive(inventoryPanel.isOpened = !inventoryPanel.isOpened);
			if(Input.GetKeyDown(KeyCode.E))
				equipmentPanel.gameObject.SetActive(equipmentPanel.isOpened = !equipmentPanel.isOpened);
		}
	}
}
