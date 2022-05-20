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

        public void ShowMessage(string msg)
        {
            if (content.childCount > 10)
                Destroy(content.GetChild(0).gameObject);
            Text text = Instantiate(messageBar, content);
            text.text = msg;
            timer = 10;
            scrollbar.value = 0f;
            gameObject.SetActive(true);
        }
    }
}
