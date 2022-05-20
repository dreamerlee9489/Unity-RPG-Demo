using UnityEngine;
using App.SO;

namespace App.Control
{
	public class NPCVentor : NPCController
	{
		protected override void Awake()
		{
			base.Awake();
			actions.Add("OpenShop", () => {
				Debug.Log("打开商店");
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Vendor_01").asset as DialogueConfig;
		}
	}
}
