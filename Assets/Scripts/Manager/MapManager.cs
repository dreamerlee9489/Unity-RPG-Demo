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
        public MapData mapData = null;
        public Dictionary<string, CombatEntity> entities = null;

        void Awake()
        {
            entities = GameObject.FindObjectsOfType<CombatEntity>().ToDictionary(entity => entity.name);
            MapData tempMapData = JsonManager.Instance.LoadData<MapData>(SceneManager.GetActiveScene().name + "MapData_" + InventoryManager.Instance.playerData.nickName);
            mapData = tempMapData == null ? new MapData() : tempMapData;
            for (int i = 0; i < mapData.mapItemDatas.Count; i++)
            {
                Item item = Instantiate(Resources.Load<Item>(mapData.mapItemDatas[i].path), new Vector3(mapData.mapItemDatas[i].position.x, mapData.mapItemDatas[i].position.y, mapData.mapItemDatas[i].position.z), Quaternion.Euler(90, 90, 90));
                item.itemData.id = mapData.mapItemDatas[i].id;
                item.itemData.path = mapData.mapItemDatas[i].path;
                item.itemData.position = mapData.mapItemDatas[i].position;
                item.itemData.containerType = mapData.mapItemDatas[i].containerType;
                item.itemData.level = mapData.mapItemDatas[i].level;
            }
        }

        void OnDestroy()
        {
            JsonManager.Instance.SaveData(mapData, SceneManager.GetActiveScene().name + "MapData_" + InventoryManager.Instance.playerData.nickName);
        }
    }
}
