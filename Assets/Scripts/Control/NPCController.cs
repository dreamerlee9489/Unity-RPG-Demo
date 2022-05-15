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

        public void GiveTask()
        {
			// dialoguesConfig = Instantiate(Resources.Load<DialoguesConfig>("Config/Dialogue/GuardDialogue_TaskGiven"));
            print("Get Quest");
        }
    }
}
