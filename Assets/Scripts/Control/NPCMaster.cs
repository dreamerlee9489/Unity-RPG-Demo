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
			actions.Add("OpenSkillShopPanel", () => {
				UIManager.Instance.skillShopPanel.BuildPanel(goods);
				GameManager.Instance.player.UnloadSkillTree();
				GameManager.Instance.player.professionConfig = Resources.LoadAsync("Config/Profession/ProfessionConfig_Warrior").asset as ProfessionConfig;
				GameManager.Instance.player.LoadSkillTree();
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Warrior_Master").asset as DialogueConfig;
		}
	}
}
