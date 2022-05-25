using System.Collections.Generic;
using UnityEngine;
using App.AI;

namespace App.Enviorment
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public CombatEntity player { get; set; }
        public List<Task> ongoingTasks { get; set; }
        public string targetPortal { get; set; }

        void Awake()
        {
            instance = this;
            ongoingTasks = new List<Task>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
