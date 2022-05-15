using UnityEngine.UI;

namespace App.UI
{
    public class WeaponItemUI : ItemUI
    {
        Text count = null;

        void Awake()
        {
            itemType = ItemType.WEAPON;
            count = transform.GetChild(0).GetComponent<Text>();
            count.text = "";
        }

        protected override void UseItem()
        {
			base.UseItem();
        }
    }
}
