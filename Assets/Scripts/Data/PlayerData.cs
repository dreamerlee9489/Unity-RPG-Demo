using App.Manager;

namespace App.Data
{
	public class PlayerData 
	{
		public string nickName = "";
		public int gold = 500;

		public void UpdateGold(int gold)
		{
			this.gold += gold;
			GameManager.Instance.canvas.goldPanel.goldText.text = this.gold.ToString();
		}
	}
}
