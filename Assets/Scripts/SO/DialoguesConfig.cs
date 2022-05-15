using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace App.SO
{
	[System.Serializable]
	public class DialogueOption
	{
		public string text = "";
		public UnityEvent action = new UnityEvent();
	}

	[System.Serializable]
	public class DialogueNode
	{
		public string dialogue = "";
		public bool hasNext = false;
		public List<DialogueOption> options = new List<DialogueOption>();
	}

    [CreateAssetMenu(fileName = "NewDialogueConfig", menuName = "Unity RPG Project/DialogueConfig", order = 0)]
    public class DialoguesConfig : ScriptableObject
    {
		public List<DialogueNode> dialogues = new List<DialogueNode>();
    }
}
