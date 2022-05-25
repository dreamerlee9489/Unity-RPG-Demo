using System;
using System.Collections.Generic;
using UnityEngine;
using App.SO;

namespace App.AI
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public abstract class NPCController : MonoBehaviour
    {
        public int index { get; set; }
        public List<Task> tasks { get; set; }
        public Dictionary<string, Action> actions { get; set; }
        public DialogueConfig dialogueConfig { get; set; }
        public Transform goods { get; set; }

        protected virtual void Awake()
        {
            index = 0;
            tasks = new List<Task>();
            actions = new Dictionary<string, Action>();
            goods = transform.GetChild(2);
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
