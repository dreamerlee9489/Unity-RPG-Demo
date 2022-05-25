using UnityEngine;
using App.SO;
using App.Enviorment;

namespace App.AI
{
	public class NPCGrocer : NPCController
	{
		protected override void Awake()
		{
			base.Awake();
			actions.Add("OpenItemShopPanel", () => {
				UIManager.Instance.itemShopPanel.BuildPanel(goods);
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Grocer_01").asset as DialogueConfig;
		}
	}
}
