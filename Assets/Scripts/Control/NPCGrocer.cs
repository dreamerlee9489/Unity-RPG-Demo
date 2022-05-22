using UnityEngine;
using App.SO;
using App.Manager;

namespace App.Control
{
	public class NPCGrocer : NPCController
	{
		protected override void Awake()
		{
			base.Awake();
			actions.Add("OpenShopPanel", () => {
				UIManager.Instance.shopPanel.BuildPanel(goods);
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Grocer_01").asset as DialogueConfig;
		}
	}
}
