using UnityEngine.UI;
using App.Control;

namespace App.UI
{
    public class TaskPanel : BasePanel
    {
        public ScrollRect scrollRect = null;
        public TaskBar taskBar = null;

        public void Add(Task task)
        {
            TaskBar bar = Instantiate(taskBar, scrollRect.content.transform);
            bar.gameObject.name = bar.chName.text = task.chName;
            bar.taskProgress.text = task.count + " / " + task.number;
        }

        public void UpdateTask(Task task)
        {
            for (int i = 0; i < scrollRect.content.transform.childCount; i++)
            {
                if (scrollRect.content.transform.GetChild(i).name == task.chName)
                    scrollRect.content.transform.GetChild(i).GetComponent<TaskBar>().taskProgress.text = task.count + " / " + task.number;
            }
        }

        public void Remove(Task task)
        {
            for (int i = 0; i < scrollRect.content.transform.childCount; i++)
            {
                if (scrollRect.content.transform.GetChild(i).name == task.chName)
                    Destroy(scrollRect.content.transform.GetChild(i).gameObject);
            }
        }
    }
}
