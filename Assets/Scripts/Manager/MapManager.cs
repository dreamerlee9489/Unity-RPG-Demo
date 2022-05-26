using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using App.Data;

namespace App.Manager
{
	public class MapManager : MonoBehaviour
	{
		static MapManager instance = null;
		public static MapManager Instance => instance;
		public Dictionary<string, ItemData> mapItemDatas = new Dictionary<string, ItemData>();

		void Awake()
		{
			instance = this;
		}

		void Start()
		{
			Dictionary<string, ItemData> temp = BinaryManager.Instance.LoadData<Dictionary<string, ItemData>>(SceneManager.GetActiveScene().name);
			if(temp != null)
			{
				mapItemDatas = temp;
				Debug.Log(mapItemDatas.Count);
				foreach (var item in mapItemDatas)
				{
					Debug.Log(item.Key);
				}
			}
		}

		void OnDestroy()
		{
			BinaryManager.Instance.SaveData(mapItemDatas, SceneManager.GetActiveScene().name);					
		}

		public void SaveData()
		{
			BinaryManager.Instance.SaveData(mapItemDatas, SceneManager.GetActiveScene().name);
		}

		public void LoadData()
		{
			mapItemDatas = BinaryManager.Instance.LoadData<Dictionary<string, ItemData>>(SceneManager.GetActiveScene().name);
		}
	}
}
