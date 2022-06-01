using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using App.Data;
using App.Items;
using App.Control;

namespace App.Manager
{
    public class MapManager : MonoBehaviour
    {
        Dictionary<string, Entity> enemies = null;
        public MapData mapData = null;

        public string accessPath
        {
            get { return InventoryManager.Instance.playerData.nickName + "_MapData_" + SceneManager.GetActiveScene().name; }
            private set {}
        }

        void Awake()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy").Select(enemy => enemy.GetComponent<Entity>()).ToDictionary(entity => entity.name);
            MapData temp = BinaryManager.Instance.LoadData<MapData>(accessPath);
            mapData = temp == null ? new MapData() : temp;
            for (int i = 0; i < mapData.mapItemDatas.Count; i++)
            {
                Item item = Instantiate(Resources.Load<Item>(mapData.mapItemDatas[i].path), new Vector3(mapData.mapItemDatas[i].position.x, mapData.mapItemDatas[i].position.y, mapData.mapItemDatas[i].position.z), Quaternion.Euler(90, 90, 90));
                item.itemData.id = mapData.mapItemDatas[i].id;
                item.itemData.path = mapData.mapItemDatas[i].path;
                item.itemData.position = mapData.mapItemDatas[i].position;
                item.itemData.containerType = mapData.mapItemDatas[i].containerType;
                item.itemData.level = mapData.mapItemDatas[i].level;
            }
            GameManager.Instance.onSavingData += SaveData;
        }

        void OnDestroy()
        {
            GameManager.Instance.onSavingData -= SaveData;
        }

        void SaveData()
        {
            foreach (var enemy in enemies)
			{
                EnemyData entityData = mapData.mapEnemyDatas[enemy.Value.name];
                entityData.currentHP = enemy.Value.currentHP;
                entityData.currentMP = enemy.Value.currentMP;
                entityData.position = new Vector(enemy.Value.transform.position);
            }
            BinaryManager.Instance.SaveData(mapData, accessPath);
        }
    }
}
