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
        public CombatEntity[] entities = null;
        public Item[] items = null;
        public Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
        public List<Task> registeredTasks = new List<Task>();

        void Awake()
        {
            instance = this;
            player = GameObject.FindWithTag("Player").GetComponent<CombatEntity>();
            entities = FindObjectsOfType<CombatEntity>();
            items = FindObjectsOfType<Item>();
            foreach (var item in items)
                objects.TryAdd(item.itemConfig.itemName, item.itemConfig.item.gameObject);
            foreach (var entity in entities)
                objects.TryAdd(entity.entityConfig.nickName, entity.entityConfig.entity.gameObject);
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            MessageManager.Instance.DispatchDelay();
        }
    }
}
