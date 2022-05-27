using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using App.Data;

namespace App.Manager
{
    public class MapManager : MonoBehaviour
    {
        static MapManager instance = null;
        public Dictionary<string, GameObject> dropItems = null;
        public Dictionary<string, GameObject> enemies = null;
        public MapData mapData = null;
        public static MapManager Instance => instance;

        void Awake()
        {
            instance = this;
            dropItems = GameObject.FindGameObjectsWithTag("Drop").ToDictionary(item => item.name);
            enemies = GameObject.FindGameObjectsWithTag("Enemy").ToDictionary(enemy => enemy.name);
        }

        void Start()
        {
            MapData tempData = BinaryManager.Instance.LoadData<MapData>(SceneManager.GetActiveScene().name);
            if (tempData != null)
            {
                mapData = tempData;
                foreach (var item in tempData.pickupItems)
                {
                    Destroy(dropItems[item.Key].gameObject);
                }
                foreach (var item in tempData.deadEnemies)
                {
                    Destroy(enemies[item.Key].gameObject);
                }
            }
            else
            {
                mapData = new MapData();
            }
        }

        void OnDestroy()
        {
            BinaryManager.Instance.SaveData(mapData, SceneManager.GetActiveScene().name);
        }
    }
}
