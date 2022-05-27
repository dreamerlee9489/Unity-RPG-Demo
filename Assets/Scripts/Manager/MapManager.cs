using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using App.Data;
using App.Items;

namespace App.Manager
{
    public class MapManager : MonoBehaviour
    {
        static MapManager instance = null;
        public static MapManager Instance => instance;
        public List<ItemData> dropItemDatas = null;
        public List<EntityData> deadEnemies = null;

        void Awake()
        {
            instance = this;
            List<ItemData> itemDatas = JsonManager.Instance.LoadData<List<ItemData>>(SceneManager.GetActiveScene().name + "Map0");
            if (itemDatas == null)
                dropItemDatas = new List<ItemData>();
            else
            {
                dropItemDatas = itemDatas;
                for (int i = 0; i < dropItemDatas.Count; i++)
                {
                    Item item = Instantiate(Resources.Load<Item>(dropItemDatas[i].path), new Vector3(dropItemDatas[i].position.x, 0.1f, dropItemDatas[i].position.z), Quaternion.Euler(90, 90, 90));
                    item.itemData = new ItemData();
                    item.itemData.id = dropItemDatas[i].id;
                    item.itemData.path = dropItemDatas[i].path;
                    item.itemData.position = dropItemDatas[i].position;
                    item.itemData.containerType = dropItemDatas[i].containerType;
                    item.itemData.level = dropItemDatas[i].level;
                }
            }
        }

        void OnDestroy()
        {
            JsonManager.Instance.SaveData(dropItemDatas, SceneManager.GetActiveScene().name + "Map0");
        }
    }
}
