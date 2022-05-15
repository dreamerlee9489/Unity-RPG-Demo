using UnityEngine;
using UnityEngine.Events;

namespace App.Control
{
    public class DialogueTrigger : MonoBehaviour
    {
        public string actionName = "";
        public UnityEvent action = new UnityEvent();

        public void Trigger(string actionName)
        {
            if (this.actionName == actionName)
            {
                action.Invoke();
            }
        }
    }
}