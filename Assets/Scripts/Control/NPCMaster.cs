using UnityEngine;
using App.Manager;
using App.SO;

namespace App.Control
{
	public class NPCMaster : NPCController
	{
		ProfessionConfig professionConfig = null;
		protected override void Awake()
		{
			base.Awake();
			professionConfig = Resources.LoadAsync("Config/Profession/ProfessionConfig_Warrior").asset as ProfessionConfig;
			actions.Add("OpenSkillShopPanel", () => {
				UIManager.Instance.skillShopPanel.BuildPanel(goods);
				if(GameManager.Instance.player.professionConfig != professionConfig)
				{
					GameManager.Instance.player.UnloadSkillTree();
					GameManager.Instance.player.professionConfig = Resources.LoadAsync("Config/Profession/ProfessionConfig_Warrior").asset as ProfessionConfig;
					GameManager.Instance.player.LoadSkillTree();
				}
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Warrior_Master").asset as DialogueConfig;
		}
	}
}
