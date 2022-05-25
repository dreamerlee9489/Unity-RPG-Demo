using UnityEngine;
using App.Enviorment;
using App.SO;

namespace App.AI
{
	public class NPCMaster : NPCController
	{
		protected override void Awake()
		{
			base.Awake();
			actions.Add("OpenSkillShopPanel", () => {
				UIManager.Instance.skillShopPanel.BuildPanel(goods);
				if(GameManager.Instance.player.professionConfig != GetComponent<CombatEntity>().professionConfig)
				{
					GameManager.Instance.player.UnloadSkillTree();
					GameManager.Instance.player.professionConfig = Resources.LoadAsync("Config/Profession/ProfessionConfig_Warrior").asset as ProfessionConfig;
				}
			});
            dialogueConfig = Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_WarriorMaster").asset as DialogueConfig;
		}
	}
}
