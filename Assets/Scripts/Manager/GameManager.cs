using System.Collections.Generic;
using UnityEngine;
using App.Control;
using App.Items;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public CombatEntity player = null;
        public List<Task> registeredTasks = new List<Task>();

        void Awake()
        {
            instance = this;
            player = GameObject.FindWithTag("Player").GetComponent<CombatEntity>();
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            MessageManager.Instance.DispatchDelay();
        }
    }
}
