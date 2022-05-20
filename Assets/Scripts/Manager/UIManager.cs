using UnityEngine;
using UnityEngine.AI;
using App.Control;
using App.UI;

namespace App.Manager
{
    public class UIManager : MonoBehaviour, ICmdReceiver
    {
        static UIManager instance = null;
        Transform dialogueTarget = null;
        public static UIManager Instance => instance;
        public HUDPanel hudPanel { get; set; }
        public BagPanel bagPanel { get; set; }
        public EquipmentPanel equipmentPanel { get; set; }
        public DialoguePanel dialoguePanel { get; set; }
        public TaskPanel taskPanel { get; set; }
        public GoldPanel goldPanel { get; set; }
        public MessagePanel messagePanel { get; set; }
        public AttributePanel attributePanel { get; set; }
        public TipPanel tipPanel { get; set; }

        void Awake()
        {
            instance = this;
            hudPanel = GameObject.Find("HUDPanel").GetComponent<HUDPanel>();
            bagPanel = GameObject.Find("BagPanel").GetComponent<BagPanel>();
            equipmentPanel = GameObject.Find("EquipmentPanel").GetComponent<EquipmentPanel>();
            dialoguePanel = GameObject.Find("DialoguePanel").GetComponent<DialoguePanel>();
            taskPanel = GameObject.Find("TaskPanel").GetComponent<TaskPanel>();
            goldPanel = GameObject.Find("GoldPanel").GetComponent<GoldPanel>();
            messagePanel = GameObject.Find("MessagePanel").GetComponent<MessagePanel>();
            attributePanel = GameObject.Find("AttributePanel").GetComponent<AttributePanel>();
            tipPanel = GameObject.Find("TipPanel").GetComponent<TipPanel>();
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            bagPanel.gameObject.SetActive(false);
            equipmentPanel.gameObject.SetActive(false);
            dialoguePanel.gameObject.SetActive(false);
            taskPanel.gameObject.SetActive(false);
            messagePanel.gameObject.SetActive(false);
            attributePanel.gameObject.SetActive(false);
            tipPanel.gameObject.SetActive(false);
        }

        void Update()
        {
            if(dialogueTarget != null)
                ExecuteAction(dialogueTarget);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bagPanel.gameObject.SetActive(bagPanel.isOpened = false);
                equipmentPanel.gameObject.SetActive(equipmentPanel.isOpened = false);
                dialoguePanel.gameObject.SetActive(dialoguePanel.isOpened = false);
                taskPanel.gameObject.SetActive(taskPanel.isOpened = false);
                messagePanel.gameObject.SetActive(messagePanel.isOpened = false);
                attributePanel.gameObject.SetActive(attributePanel.isOpened = false);
            }
            if (Input.GetKeyDown(KeyCode.A))
                attributePanel.gameObject.SetActive(attributePanel.isOpened = !attributePanel.isOpened);
            if (Input.GetKeyDown(KeyCode.B))
                bagPanel.gameObject.SetActive(bagPanel.isOpened = !bagPanel.isOpened);
            if (Input.GetKeyDown(KeyCode.E))
                equipmentPanel.gameObject.SetActive(equipmentPanel.isOpened = !equipmentPanel.isOpened);
            if (Input.GetKeyDown(KeyCode.Q))
                taskPanel.gameObject.SetActive(taskPanel.isOpened = !taskPanel.isOpened);
        }

        public void ExecuteAction(Vector3 point) { }
        public void ExecuteAction(Transform target)
        {
            if(!GameManager.Instance.player.CanAttack(target))
            {
                dialogueTarget = target;
                GameManager.Instance.player.GetComponent<NavMeshAgent>().destination = target.position;
                GameManager.Instance.player.transform.LookAt(target);
            }
            else
            {
                dialoguePanel.npc = target.GetComponent<NPCController>();
                target.LookAt(GameManager.Instance.player.transform);
                dialoguePanel.gameObject.SetActive(true);
                dialogueTarget = null;
            }
        }

        public void CancelAction()
        {
            dialoguePanel.gameObject.SetActive(false);
        }
    }
}
