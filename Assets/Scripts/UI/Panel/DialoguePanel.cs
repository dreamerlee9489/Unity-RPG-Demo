using UnityEngine;
using UnityEngine.UI;
using App.SO;
using App.Control;

namespace App.UI
{
    public class DialoguePanel : BasePanel
    {
        Button nextBtn = null;
        public Text npcName = null;
        public Text dialogue = null;
        public GameObject nextRow = null;
        public GameObject choices = null;
        public Button quitBtn = null;
        DialoguesConfig dialoguesConfig = null;
        [HideInInspector] public NPCController npc = null;
        int index = 0;

        void Awake()
        {
            nextBtn = nextRow.transform.GetChild(0).GetComponent<Button>();
            nextBtn.onClick.AddListener(() =>
            {
                DialogueNode node = dialoguesConfig.dialogues[++index];
                dialogue.text = node.dialogue;
                nextRow.SetActive(node.hasNext);
                if (node.options.Count > 0)
                {
                    for (int i = 0; i < node.options.Count; i++)
                    {
                        int n = i;
                        Button btn = choices.transform.GetChild(i).GetComponent<Button>();
                        btn.gameObject.SetActive(true);
                        btn.transform.GetChild(0).GetComponent<Text>().text = node.options[i].dialogue;
                        btn.onClick.AddListener(() =>
                        {
                            npc.ActionTrigger(node.options[n].action);
                            gameObject.SetActive(false);
                        });
                    }
                }
            });
            quitBtn.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
            nextRow.SetActive(false);
        }

        void OnEnable()
        {
            npcName.text = npc.gameObject.name;
            dialoguesConfig = npc.dialoguesConfig;
            DialogueNode node = dialoguesConfig.dialogues[0];
            dialogue.text = node.dialogue;
            nextRow.SetActive(node.hasNext);
            if (node.options.Count > 0)
            {
                for (int i = 0; i < node.options.Count; i++)
                {
                    int n = i;
                    Button btn = choices.transform.GetChild(i).GetComponent<Button>();
                    btn.gameObject.SetActive(true);
                    btn.transform.GetChild(0).GetComponent<Text>().text = node.options[i].dialogue;
                    btn.onClick.AddListener(() =>
                    {
                        npc.ActionTrigger(node.options[n].action);
                        gameObject.SetActive(false);
                    });
                }
            }
        }

        void OnDisable()
        {
            index = 0;
            for (int i = 0; i < choices.transform.childCount; i++)
            {
                Button btn = choices.transform.GetChild(i).GetComponent<Button>();
                btn.onClick.RemoveAllListeners();
                btn.gameObject.SetActive(false);
            }
        }
    }
}
