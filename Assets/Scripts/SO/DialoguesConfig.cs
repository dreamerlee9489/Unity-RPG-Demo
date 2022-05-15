using System.Collections.Generic;
using UnityEngine;

namespace App.SO
{
	[System.Serializable]
	public class DialogueNode
	{
		public string dialogue = "";
		public bool hasNext = false;
		public List<string> choices = new List<string>();
	}

    [CreateAssetMenu(fileName = "NewDialogueConfig", menuName = "Unity RPG Project/DialogueConfig", order = 0)]
    public class DialoguesConfig : ScriptableObject
    {
		public List<DialogueNode> dialogues = new List<DialogueNode>();
    }
}
