using System.Collections.Generic;
using UnityEngine;
using App.Control;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public CombatEntity player { get; set; }
        public List<Task> ongoingTasks { get; set; }

        void Awake()
        {
            instance = this;
            player = GameObject.FindObjectOfType<PlayerController>().GetComponent<CombatEntity>();
            ongoingTasks = new List<Task>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
