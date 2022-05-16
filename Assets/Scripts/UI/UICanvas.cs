using App.Control;
using UnityEngine;

namespace App.UI
{
    public class UICanvas : MonoBehaviour, ICmdReceiver
    {
        public BagPanel bagPanel = null;
        public EquipmentPanel equipmentPanel = null;
        public DialoguePanel dialoguePanel = null;
        public QuestPanel questPanel = null;

        void Awake()
        {
            bagPanel.gameObject.SetActive(false);
            equipmentPanel.gameObject.SetActive(false);
            dialoguePanel.gameObject.SetActive(false);
            questPanel.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
                bagPanel.gameObject.SetActive(bagPanel.isOpened = !bagPanel.isOpened);
            if (Input.GetKeyDown(KeyCode.E))
                equipmentPanel.gameObject.SetActive(equipmentPanel.isOpened = !equipmentPanel.isOpened);
            if (Input.GetKeyDown(KeyCode.Q))
                questPanel.gameObject.SetActive(questPanel.isOpened = !questPanel.isOpened);
        }
		
        public void ExecuteAction(Vector3 point)
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteAction(Transform target)
        {
            dialoguePanel.npc = target.GetComponent<NPCController>();
            dialoguePanel.gameObject.SetActive(true);
        }

        public void CancelAction()
        {
            dialoguePanel.gameObject.SetActive(false);
        }
    }
}
