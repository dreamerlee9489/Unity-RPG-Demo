using UnityEngine.UI;
using App.Manager;

namespace App.UI
{
	public class GoldPanel : BasePanel
	{
		public Text goldText = null;

		void Start()
		{
			goldText.text = InventoryManager.Instance.golds.ToString();
		}

		public void UpdatePanel()
		{
			goldText.text = InventoryManager.Instance.golds.ToString();
		}
	}
}
