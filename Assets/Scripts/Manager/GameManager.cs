using System.Collections.Generic;
using UnityEngine;
using App.Control;
using App.Data;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public string targetPortal { get; set; }
        public CombatEntity player { get; set; }
        public PlayerData currentPlayerData { get; set; }
        public List<Task> ongoingTasks { get; set; }

        void Awake()
        {
            instance = this;
            ongoingTasks = new List<Task>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
