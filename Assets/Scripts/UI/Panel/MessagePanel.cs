﻿using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MessagePanel : BasePanel
    {
        float timer = 0;
        ScrollRect scrollRect { get; set; }
        public Text messageBar { get; set; }

        void Awake()
        {
            scrollRect = gameObject.GetComponentInChildren<ScrollRect>();
            messageBar = Resources.Load<Text>("UI/Bar/MessageBar");
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

        public void Print(string msg, Color color)
        {
            if (scrollRect.content.childCount > 6)
                Destroy(scrollRect.content.GetChild(0).gameObject);
            Text text = Instantiate(messageBar, scrollRect.content);
            text.color = color;
            text.text = msg;
            timer = 10;
            scrollRect.verticalScrollbar.value = 0f;
            gameObject.SetActive(true);
        }
    }
}