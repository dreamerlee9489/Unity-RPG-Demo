using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class MessagePanel : BasePanel
    {
        float timer = 0;
        public Transform content = null;
        public Scrollbar scrollbar = null;
        public Text messageBar = null;

        void Awake()
        {
            scrollbar.onValueChanged.AddListener((float value) =>
            {
                scrollbar.value = value;
            });
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
            if (content.childCount > 6)
                Destroy(content.GetChild(0).gameObject);
            Text text = Instantiate(messageBar, content);
            text.color = color;
            text.text = msg;
            gameObject.SetActive(true);
            timer = 10;
            scrollbar.value = 0f;        
        }
    }
}
