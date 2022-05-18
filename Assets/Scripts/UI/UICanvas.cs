using UnityEngine;
using App.Control;

namespace App.UI
{
    public class UICanvas : MonoBehaviour, ICmdReceiver
    {
        public BagPanel bagPanel { get; set; }
        public EquipmentPanel equipmentPanel { get; set; }
        public DialoguePanel dialoguePanel { get; set; }
        public QuestPanel questPanel { get; set; }
        public GoldPanel goldPanel { get; set; }

        void Awake()
        {
            bagPanel = GameObject.Find("BagPanel").GetComponent<BagPanel>();
            equipmentPanel = GameObject.Find("EquipmentPanel").GetComponent<EquipmentPanel>();
            dialoguePanel = GameObject.Find("DialoguePanel").GetComponent<DialoguePanel>();
            questPanel = GameObject.Find("QuestPanel").GetComponent<QuestPanel>();
            goldPanel = GameObject.Find("GoldPanel").GetComponent<GoldPanel>();
        }

        void Start()
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
