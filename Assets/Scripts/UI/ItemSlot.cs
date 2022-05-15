using UnityEngine;

namespace App.UI
{
	public enum ItemType { ALL, HELMET, ARMOR, SHIELD, BOOTS, NECKLACE, BRACER, WEAPON, PANTS, POTION }

	public class ItemSlot : MonoBehaviour
	{
		public ItemType itemType = ItemType.ALL;
	}
}
