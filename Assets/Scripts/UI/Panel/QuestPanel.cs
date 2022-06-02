using UnityEngine;
using UnityEngine.UI;
using App.Control;

namespace App.UI
{
    public class QuestPanel : BasePanel
    {
        ScrollRect scrollRect = null;
        QuestBar questBar = null;
        QuestBar tempBar = null;

        void Awake()
        {
            scrollRect = gameObject.GetComponentInChildren<ScrollRect>();
            questBar = Resources.Load<QuestBar>("UI/Bar/QuestBar");
        }

        public void Add(Quest quest)
        {
            QuestBar bar = Instantiate(questBar, scrollRect.content);
            bar.chName.text = quest.chName;
            bar.progress.text = quest.count + " / " + quest.number;
        }

        public void UpdatePanel(Quest quest)
        {
            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                tempBar = scrollRect.content.GetChild(i).GetComponent<QuestBar>();
                if (tempBar.chName.text == quest.chName)
                {
                    tempBar.progress.text = quest.count + " / " + quest.number;
                    return;
                }
            }
        }

        public void Remove(Quest quest)
        {
            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                tempBar = scrollRect.content.GetChild(i).GetComponent<QuestBar>();
                if (tempBar.chName.text== quest.chName)
                {
                    Destroy(scrollRect.content.GetChild(i).gameObject);
                    return;
                }
            }
        }
    }
}
