using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using App.Control;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public CombatEntity player = null;
        public Dictionary<string, MoveEntity> entities;
        public List<Quest> ongoingQuests = new List<Quest>();

        void Awake()
        {
            instance = this;
            player = GameObject.FindWithTag("Player").GetComponent<CombatEntity>();
            entities = GameObject.FindObjectsOfType<MoveEntity>().ToDictionary(entity => entity.nickName);
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            MessageManager.Instance.DispatchDelay();
        }
    }
}
