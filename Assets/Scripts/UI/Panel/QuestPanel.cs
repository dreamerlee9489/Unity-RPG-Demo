using UnityEngine;
using UnityEngine.UI;
using App.Control;

namespace App.UI
{
	public class QuestPanel : BasePanel
	{
		public ScrollRect scrollRect = null;

		public void Add(Quest quest)
		{
			QuestBar bar = Instantiate(Resources.Load<QuestBar>("UI/QuestBar"), scrollRect.content.transform);
			bar.gameObject.name = bar.questName.text = quest.name;
			bar.questProgress.text = quest.current + " / " + quest.total; 
		}

		public void UpdateQuest(Quest quest)
		{
			for (int i = 0; i < scrollRect.content.transform.childCount; i++)
			{
				if(scrollRect.content.transform.GetChild(i).name == quest.name)
					scrollRect.content.transform.GetChild(i).GetComponent<QuestBar>().questProgress.text = quest.current + " / " + quest.total; 
			}
		}

		public void Remove(Quest quest)
		{
			for (int i = 0; i < scrollRect.content.transform.childCount; i++)
			{
				if(scrollRect.content.transform.GetChild(i).name == quest.name)
					Destroy(scrollRect.content.transform.GetChild(i).gameObject);
			}
		}
	}
}
