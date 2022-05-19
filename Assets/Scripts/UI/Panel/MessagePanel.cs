using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public class MessagePanel : BasePanel
    {
        bool isShow = false;
        float timer = 0;
        public Transform content = null;
        public Scrollbar scrollbar = null;

        private void Awake()
        {
            scrollbar.onValueChanged.AddListener((float value) =>
            {
                scrollbar.value = value;
            });
        }

        private void Update()
        {
            if (isShow)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    isShow = false;
                    gameObject.SetActive(false);
                }
            }
        }

        public void ShowMessage(string msg)
        {
            if (content.childCount > 10)
                Destroy(content.GetChild(0).gameObject);
            Text text = Instantiate(Resources.Load<Text>("UI/Message"), content);
            text.text = msg;
            scrollbar.value = 0f;
            isShow = true;
            timer = 10;
            gameObject.SetActive(true);
        }
    }
}
