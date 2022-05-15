using UnityEngine;
using App.SO;
using UnityEditor;

namespace App.Control
{
    public class NPCController : MonoBehaviour
    {
        public DialoguesConfig dialoguesConfig = null;

        void Awake()
        {
			// dialoguesConfig = Resources.Load<DialoguesConfig>("Config/Dialogue/GuardDialogue_TaskNotGiven");
        }

        public void GiveQuest()
        {
			dialoguesConfig = Resources.Load<DialoguesConfig>("Config/Dialogue/GuardDialogue_QuestGiven");
            print("Get Quest");
        }
    }
}
