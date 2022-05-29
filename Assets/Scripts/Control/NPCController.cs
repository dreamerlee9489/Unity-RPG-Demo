using System;
using System.Collections.Generic;
using UnityEngine;
using App.SO;

namespace App.Control
{
    [RequireComponent(typeof(MoveEntity), typeof(CombatEntity))]
    public abstract class NPCController : MonoBehaviour
    {
        protected string nickName = "";
        public Transform goods { get; set; }
        public DialogueConfig dialogueConfig { get; set; }
        public Dictionary<string, Action> actions = new Dictionary<string, Action>();

        protected virtual void Awake()
        {
            goods = transform.GetChild(2);
            nickName = GetComponent<CombatEntity>().entityConfig.nickName;
        }

        public void ActionTrigger(string action)
        {
            if (actions.ContainsKey(action))
                actions[action].Invoke();
        }
    }
}
