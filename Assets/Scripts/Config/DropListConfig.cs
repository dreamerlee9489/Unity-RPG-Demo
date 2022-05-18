using System.Collections.Generic;
using UnityEngine;
using App.Items;

namespace App.Config
{	
	[CreateAssetMenu(fileName = "DropListConfig_", menuName = "Unity RPG Project/DropListConfig", order = 5)]
	public class DropListConfig : ScriptableObject 
	{
		public const int MAX_DROP_COUNT = 3;
		public List<GameItem> dropList = new List<GameItem>(); 

		public List<GameItem> GetDrops(AbilityConfig enemyConfig, out int bounty)
		{
			bounty = (int)Random.Range(enemyConfig.hp * 0.5f, enemyConfig.hp * 2f);
			List<GameItem> drops = new List<GameItem>();
			int dropCount = Random.Range(1, MAX_DROP_COUNT);
			for (int i = 0; i < dropCount; i++)
				drops.Add(dropList[Random.Range(0, dropList.Count)]);
			return drops;
		}
	}
}
