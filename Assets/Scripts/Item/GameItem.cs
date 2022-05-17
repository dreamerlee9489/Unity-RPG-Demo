using UnityEngine;
using App.SO;
using App.UI;

namespace App.Item
{
    public abstract class GameItem : MonoBehaviour
    {
        public ItemConfig itemConfig = null;
        public ItemUI itemUI = null;
        public ItemType itemType = ItemType.WEAPON;
        public PanelType panelType = PanelType.NONE;

        public GameItem(ItemConfig itemConfig)
        {
            panelType = PanelType.NONE;
            this.itemConfig = itemConfig;
            GetComponent<Collider>().enabled = true;
        }

        protected abstract void OnTriggerEnter(Collider other);
        public abstract void Use(Transform user);
    }
}
