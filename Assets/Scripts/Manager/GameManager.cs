using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using App.Control;
using App.UI;

namespace App.Manager
{
    public class GameManager : MonoBehaviour
    {
        static GameManager instance = null;
        public static GameManager Instance => instance;
        public UICanvas canvas = null;
        public CombatEntity player = null;
        public Dictionary<string, MoveEntity> entities;
        public List<Quest> ongoingQuests = new List<Quest>();

        void Awake()
        {
            instance = this;
            canvas = GameObject.Find("UICanvas").GetComponent<UICanvas>();
            player = GameObject.FindWithTag("Player").GetComponent<CombatEntity>();
            entities = GameObject.FindObjectsOfType<MoveEntity>().ToDictionary(entity => entity.name);
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            MessageManager.Instance.DispatchDelay();
        }
    }
}
