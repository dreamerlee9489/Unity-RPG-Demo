using System;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace Control
{
    [RequireComponent(typeof(Entity))]
    public abstract class NPCController : MonoBehaviour
    {
        protected string nickName = "";
        public Transform goods { get; set; }
        public DialogueConfig dialogueConfig { get; set; }
        public Dictionary<string, Action> actions = new Dictionary<string, Action>();

        protected virtual void Awake()
        {
            goods = transform.GetChild(2);
            nickName = GetComponent<Entity>().entityConfig.nickName;
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}