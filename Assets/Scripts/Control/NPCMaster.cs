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
			actions.Add("OpenSkillPanel", () => {
				UIManager.Instance.skillPanel.BuildPanel(goods);
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Warrior_Master").asset as DialogueConfig;
		}
	}
}
