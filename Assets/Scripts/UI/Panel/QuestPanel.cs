using UnityEngine;
using UnityEngine.UI;
using App.Control;

namespace App.UI
{
    public class QuestPanel : BasePanel
    {
        public ScrollRect scrollRect = null;
        public QuestBar questBar = null;

        public void Add(Quest quest)
        {
            QuestBar bar = Instantiate(questBar, scrollRect.content.transform);
            bar.gameObject.name = bar.chName.text = quest.chName;
            bar.questProgress.text = quest.current + " / " + quest.total;
        }

        public void UpdateQuest(Quest quest)
        {
            for (int i = 0; i < scrollRect.content.transform.childCount; i++)
            {
                if (scrollRect.content.transform.GetChild(i).name == quest.chName)
                    scrollRect.content.transform.GetChild(i).GetComponent<QuestBar>().questProgress.text = quest.current + " / " + quest.total;
            }
        }

        public void Remove(Quest quest)
        {
            for (int i = 0; i < scrollRect.content.transform.childCount; i++)
            {
                if (scrollRect.content.transform.GetChild(i).name == quest.chName)
                    Destroy(scrollRect.content.transform.GetChild(i).gameObject);
            }
        }
    }
}
