using System.Collections.Generic;
using UnityEngine;

namespace App.SO
{
    [System.Serializable]
    public class DialogueOption
    {
        public string dialogue = "";
        public string action = "";
    }

    [System.Serializable]
    public class DialogueNode
    {
        public string dialogue = "";
        public bool hasNext = false;
        public List<DialogueOption> options = new List<DialogueOption>();
    }

    [CreateAssetMenu(fileName = "DialogueConfig_", menuName = "Unity RPG Project/DialogueConfig", order = 1)]
    public class DialoguesConfig : ScriptableObject
    {
        public List<DialogueNode> dialogues = new List<DialogueNode>();
    }
}
