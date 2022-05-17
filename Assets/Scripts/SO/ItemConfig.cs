using UnityEngine;
using App.UI;
using App.Item;

namespace App.SO
{
    public abstract class ItemConfig : ScriptableObject
    {
        public new string name = "";
        public string description = "";
        public string path = "";
    }
}
