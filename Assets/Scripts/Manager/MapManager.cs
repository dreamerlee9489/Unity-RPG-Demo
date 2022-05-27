using System;
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
        static MapManager instance = null;
        Dictionary<string, CombatEntity> entities = null;
        public MapData mapData = null;
        public static MapManager Instance => instance;

        void Awake()
        {
            instance = this;
            mapData = new MapData();
            entities = GameObject.FindObjectsOfType<CombatEntity>().ToDictionary(entity => entity.name);
            List<ItemData> itemDatas = BinaryManager.Instance.LoadData<List<ItemData>>(SceneManager.GetActiveScene().name + "Map0");
            Dictionary<string, EntityData> entityDatas = BinaryManager.Instance.LoadData<Dictionary<string, EntityData>>(SceneManager.GetActiveScene().name + "Map1");
            mapData.mapItemDatas = itemDatas == null ? new List<ItemData>() : itemDatas;
            mapData.mapEntityDatas = entityDatas == null ? new Dictionary<string, EntityData>() : entityDatas;
            for (int i = 0; i < mapData.mapItemDatas.Count; i++)
            {
                Item item = Instantiate(Resources.Load<Item>(mapData.mapItemDatas[i].path), new Vector3(mapData.mapItemDatas[i].position.x, 0.1f, mapData.mapItemDatas[i].position.z), Quaternion.Euler(90, 90, 90));
                item.itemData = new ItemData();
                item.itemData.id = mapData.mapItemDatas[i].id;
                item.itemData.path = mapData.mapItemDatas[i].path;
                item.itemData.position = mapData.mapItemDatas[i].position;
                item.itemData.containerType = mapData.mapItemDatas[i].containerType;
                item.itemData.level = mapData.mapItemDatas[i].level;
            }
        }

        void OnDestroy()
        {
            BinaryManager.Instance.SaveData(mapData.mapItemDatas, SceneManager.GetActiveScene().name + "Map0");
            BinaryManager.Instance.SaveData(mapData.mapEntityDatas, SceneManager.GetActiveScene().name + "Map1");
        }
    }
}
