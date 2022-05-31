using UnityEngine;
using App.Control;
using App.UI;

namespace App.Manager
{
    public class UIManager : MonoBehaviour, ICmdReceiver
    {
        static UIManager instance = null;
        public static UIManager Instance => instance;
        public Transform target { get; set; }
        public StartPanel startPanel { get; set; }
        public HUDPanel hudPanel { get; set; }
        public ActionPanel actionPanel { get; set; }
        public BagPanel bagPanel { get; set; }
        public EquipmentPanel equipmentPanel { get; set; }
        public DialoguePanel dialoguePanel { get; set; }
        public TaskPanel taskPanel { get; set; }
        public GoldPanel goldPanel { get; set; }
        public MessagePanel messagePanel { get; set; }
        public AttributePanel attributePanel { get; set; }
        public TipPanel tipPanel { get; set; }
        public ItemShopPanel itemShopPanel { get; set; }
        public SkillShopPanel skillShopPanel { get; set; }
        public PausePanel pausePanel { get; set; }
        public AudioSource audioSource { get; set; }

        void Awake()
        {
            instance = this;
            startPanel = GameObject.Find("StartPanel").GetComponent<StartPanel>();
            hudPanel = GameObject.Find("HUDPanel").GetComponent<HUDPanel>();
            actionPanel = GameObject.Find("ActionPanel").GetComponent<ActionPanel>();
            bagPanel = GameObject.Find("BagPanel").GetComponent<BagPanel>();
            equipmentPanel = GameObject.Find("EquipmentPanel").GetComponent<EquipmentPanel>();
            dialoguePanel = GameObject.Find("DialoguePanel").GetComponent<DialoguePanel>();
            taskPanel = GameObject.Find("TaskPanel").GetComponent<TaskPanel>();
            goldPanel = GameObject.Find("GoldPanel").GetComponent<GoldPanel>();
            messagePanel = GameObject.Find("MessagePanel").GetComponent<MessagePanel>();
            attributePanel = GameObject.Find("AttributePanel").GetComponent<AttributePanel>();
            tipPanel = GameObject.Find("TipPanel").GetComponent<TipPanel>();
            itemShopPanel = GameObject.Find("ItemShopPanel").GetComponent<ItemShopPanel>();
            skillShopPanel = GameObject.Find("SkillShopPanel").GetComponent<SkillShopPanel>();
            pausePanel = GameObject.Find("PausePanel").GetComponent<PausePanel>();
            startPanel.gameObject.SetActive(true);
            hudPanel.gameObject.SetActive(false);
            actionPanel.gameObject.SetActive(false);
            goldPanel.gameObject.SetActive(false);
            bagPanel.gameObject.SetActive(false);
            equipmentPanel.gameObject.SetActive(false);
            dialoguePanel.gameObject.SetActive(false);
            taskPanel.gameObject.SetActive(false);
            messagePanel.gameObject.SetActive(false);
            attributePanel.gameObject.SetActive(false);
            tipPanel.gameObject.SetActive(false);
            itemShopPanel.gameObject.SetActive(false);
            skillShopPanel.gameObject.SetActive(false);
            pausePanel.gameObject.SetActive(false);
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (!startPanel.gameObject.activeSelf)
            {
                if (target != null)
                    ExecuteAction(target);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    bagPanel.gameObject.SetActive(bagPanel.isOpened = false);
                    equipmentPanel.gameObject.SetActive(equipmentPanel.isOpened = false);
                    dialoguePanel.gameObject.SetActive(dialoguePanel.isOpened = false);
                    taskPanel.gameObject.SetActive(taskPanel.isOpened = false);
                    messagePanel.gameObject.SetActive(messagePanel.isOpened = false);
                    attributePanel.gameObject.SetActive(attributePanel.isOpened = false);
                    itemShopPanel.gameObject.SetActive(itemShopPanel.isOpened = false);
                    skillShopPanel.gameObject.SetActive(skillShopPanel.isOpened = false);
                    pausePanel.gameObject.SetActive(pausePanel.isOpened = true);
                }
                if (Input.GetKeyDown(KeyCode.A))
                    attributePanel.gameObject.SetActive(attributePanel.isOpened = !attributePanel.isOpened);
                if (Input.GetKeyDown(KeyCode.B))
                    bagPanel.gameObject.SetActive(bagPanel.isOpened = !bagPanel.isOpened);
                if (Input.GetKeyDown(KeyCode.E))
                    equipmentPanel.gameObject.SetActive(equipmentPanel.isOpened = !equipmentPanel.isOpened);
                if (Input.GetKeyDown(KeyCode.Q))
                    taskPanel.gameObject.SetActive(taskPanel.isOpened = !taskPanel.isOpened);
                if (Input.GetKeyDown(KeyCode.K))
                    skillShopPanel.gameObject.SetActive(skillShopPanel.isOpened = !skillShopPanel.isOpened);
                if (Input.GetKeyDown(KeyCode.P))
                    pausePanel.gameObject.SetActive(pausePanel.isOpened = !pausePanel.isOpened);
            }
        }

        public void ExecuteAction(Vector3 point) { }
        public void ExecuteAction(Transform target)
        {
            if (!GameManager.Instance.player.GetComponent<MoveEntity>().CanDialogue(target))
            {
                this.target = target;
                GameManager.Instance.player.agent.destination = target.position;
            }
            else
            {
                GameManager.Instance.player.transform.LookAt(target);
                target.LookAt(GameManager.Instance.player.transform);
                dialoguePanel.npc = target.GetComponent<NPCController>();
                dialoguePanel.gameObject.SetActive(true);
                this.target = null;
            }
        }

        public void CancelAction()
        {
            dialoguePanel.gameObject.SetActive(false);
        }
    }
}
