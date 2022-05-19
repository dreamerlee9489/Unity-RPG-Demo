using UnityEngine;
using App.Items;
using System.Collections.Generic;

namespace App.Config
{
    [CreateAssetMenu(fileName = "GameItemTable", menuName = "Unity RPG Project/GameItemTable", order = 0)]
    public class GameItemTable : ScriptableObject
    {
        public Dictionary<string, GameItem> items = new Dictionary<string, GameItem>();

        public void SetKeys()
        {
            foreach (var item in items)
            {
                //item.Key = item.Value.config.itemName;
            }
        }
    }
}
