using UnityEngine.UI;

namespace App.UI
{
	public class PotionItemUI : ItemUI
	{
        Text count = null;

		void Awake()
		{
			itemType = ItemType.POTION;
            count = transform.GetChild(0).GetComponent<Text>();
            count.text = "99";		
		}

		protected override void UseItem()
		{
			base.UseItem();
			count.text = (int.Parse(count.text) - 1).ToString();
		}
	}
}
