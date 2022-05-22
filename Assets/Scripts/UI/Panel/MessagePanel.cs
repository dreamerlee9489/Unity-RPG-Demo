using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class MessagePanel : BasePanel
    {
        float timer = 0;
        ScrollRect scrollRect { get; set; }
        public Text messageBar { get; set; }

        void Awake()
        {
            scrollRect = gameObject.GetComponentInChildren<ScrollRect>();
            messageBar = Resources.Load<Text>("UI/MessageBar");
        }

        void Update()
        {
            if (gameObject.activeSelf)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    gameObject.SetActive(false);
            }
        }

        public void ShowMessage(string msg, Color color)
        {
            if (scrollRect.content.childCount > 6)
                Destroy(scrollRect.content.GetChild(0).gameObject);
            Text text = Instantiate(messageBar, scrollRect.content);
            text.color = color;
            text.text = msg;
            gameObject.SetActive(true);
            scrollRect.verticalScrollbar.value = 0f;        
            timer = 10;
        }
    }
}
