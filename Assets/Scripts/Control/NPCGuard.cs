using System.Collections.Generic;
using UnityEngine;
using App.Manager;
using App.SO;

namespace App.Control
{
	public class NPCGuard : NPCController
	{
		protected override void Awake()
		{
			base.Awake();
			actions.Add("GiveQuest_KillUndeadKnight", () =>
            {
                GiveQuest("KillUndeadKnight", "消灭不死骑士", 500, 100, 1, GameManager.Instance.objects["不死骑士"], new Dictionary<string, int>(){
                    { "Weapon_Sword_Broad", 1 }, { "Potion_Meat_01", 10 }
                });
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward("CollectMeat");
            });
            actions.Add("GiveQuest_CollectMeat", () =>
            {
                GiveQuest("CollectMeat", "收集烤牛排",  500, 200, 12, GameManager.Instance.objects["香喷喷的烤牛排"], new Dictionary<string, int>() {
                    { "Weapon_Axe_Large_01", 1 }
                });
            });
            actions.Add("GiveReward_CollectMeat", () =>
            {
                GiveReward();
            });
            dialogueConfig = index == 0 ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + quests[index].name + "_Start").asset as DialogueConfig;
		}
	}
}
