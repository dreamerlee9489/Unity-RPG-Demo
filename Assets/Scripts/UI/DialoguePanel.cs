using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using App.SO;

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
        [HideInInspector] public DialoguesConfig dialoguesConfig = null;
        int index = 0;

        void Awake()
        {
            nextBtn = nextRow.transform.GetChild(0).GetComponent<Button>();
            nextBtn.onClick.AddListener(() =>
            {
                DialogueNode node = dialoguesConfig.dialogues[++index];
                dialogue.text = node.dialogue;
                nextRow.SetActive(node.hasNext);
                if (node.choices.Count > 0)
                {
                    choices.SetActive(true);
                    for (int i = 0; i < node.choices.Count; i++)
                    {
                        // Button btn = Instantiate(Resources.Load<Button>("UI/ChoiceButton"), choices.transform);
                        Button btn = choices.transform.GetChild(i).GetComponent<Button>();
                        btn.gameObject.SetActive(true);
                        btn.transform.GetChild(0).GetComponent<Text>().text = node.choices[i];
                    }
                }
                else
                {
                    choices.SetActive(false);
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
            DialogueNode node = dialoguesConfig.dialogues[0];
            dialogue.text = node.dialogue;
            nextRow.SetActive(node.hasNext);
            if (node.choices.Count > 0)
            {
                choices.SetActive(true);
                for (int i = 0; i < node.choices.Count; i++)
                {
                    // Button btn = Instantiate(Resources.Load<Button>("UI/ChoiceButton"), choices.transform);
                    Button btn = choices.transform.GetChild(i).GetComponent<Button>();
                    btn.gameObject.SetActive(true);
                    btn.transform.GetChild(0).GetComponent<Text>().text = node.choices[i];
                }
            }
            else
            {
                choices.SetActive(false);
            }
        }

        void OnDisable()
        {
            index = 0;
            for (int i = 0; i < choices.transform.childCount; i++)
            {
                choices.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
