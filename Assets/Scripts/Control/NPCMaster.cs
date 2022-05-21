using UnityEngine;
using App.Manager;
using App.SO;

namespace App.Control
{
	public class NPCMaster : NPCController
	{
		protected override void Awake()
		{
			base.Awake();
			actions.Add("OpenSkillTree", () => {
				UIManager.Instance.shopPanel.BuildPanel(goods);
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Warrior_Master").asset as DialogueConfig;
		}
	}
}
