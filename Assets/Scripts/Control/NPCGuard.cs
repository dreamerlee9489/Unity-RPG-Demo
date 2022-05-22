using System.Collections.Generic;
using UnityEngine;
using App.SO;

namespace App.Control
{
	public class NPCGuard : NPCController
	{
		protected override void Awake()
		{
			actions.Add("GiveTask_KillUndeadKnight", () =>
            {
                GiveTask("KillUndeadKnight", "消灭不死骑士", 500, 100, 1, Resources.LoadAsync("Entity/Enemy_UndeadKnight_01").asset as GameObject, new Dictionary<string, int>(){
                    { "Weapon_Sword_Broad", 1 }, { "Potion_Meat_01", 10 }
                });
            });
            actions.Add("GiveReward_KillUndeadKnight", () =>
            {
                GiveReward("CollectMeat");
            });
            actions.Add("GiveTask_CollectMeat", () =>
            {
                GiveTask("CollectMeat", "收集烤牛排",  500, 200, 12, Resources.LoadAsync("Items/Potion_Meat_01").asset as GameObject, new Dictionary<string, int>() {
                    { "Weapon_Axe_Large_01", 1 }
                });
            });
            actions.Add("GiveReward_CollectMeat", () =>
            {
                GiveReward();
            });
            dialogueConfig = index == 0 ? Resources.LoadAsync("Config/Dialogue/DialogueConfig_KillUndeadKnight_Start").asset as DialogueConfig : Resources.LoadAsync("Config/Dialogue/DialogueConfig_" + tasks[index].name + "_Start").asset as DialogueConfig;
		}
	}
}
