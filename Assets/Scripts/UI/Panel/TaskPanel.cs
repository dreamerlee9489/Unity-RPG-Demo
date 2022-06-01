using UnityEngine;
using UnityEngine.UI;
using App.Control;

namespace App.UI
{
    public class TaskPanel : BasePanel
    {
        ScrollRect scrollRect = null;
        TaskBar taskBar = null;
        TaskBar tempBar = null;

        void Awake()
        {
            scrollRect = gameObject.GetComponentInChildren<ScrollRect>();
            taskBar = Resources.Load<TaskBar>("UI/Bar/TaskBar");
        }

        public void Add(Quest task)
        {
            TaskBar bar = Instantiate(taskBar, scrollRect.content);
            bar.chName.text = task.chName;
            bar.progress.text = task.count + " / " + task.number;
        }

        public void UpdatePanel(Quest task)
        {
            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                tempBar = scrollRect.content.GetChild(i).GetComponent<TaskBar>();
                if (tempBar.chName.text == task.chName)
                {
                    tempBar.progress.text = task.count + " / " + task.number;
                    return;
                }
            }
        }

        public void Remove(Quest task)
        {
            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                tempBar = scrollRect.content.GetChild(i).GetComponent<TaskBar>();
                if (tempBar.chName.text== task.chName)
                {
                    Destroy(scrollRect.content.GetChild(i).gameObject);
                    return;
                }
            }
        }
    }
}
