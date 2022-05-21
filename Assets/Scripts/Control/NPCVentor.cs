using UnityEngine;
using App.SO;
using App.Manager;

namespace App.Control
{
	public class NPCVentor : NPCController
	{
		protected override void Awake()
		{
			base.Awake();
			actions.Add("OpenShop", () => {
				UIManager.Instance.shopPanel.BuildPanel(goods);
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Vendor_01").asset as DialogueConfig;
		}
	}
}
