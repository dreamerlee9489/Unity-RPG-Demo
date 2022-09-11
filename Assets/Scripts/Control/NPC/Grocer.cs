using Manager;
using SO;
using UnityEngine;

namespace Control.NPC
{
    public class Grocer : NPCController
    {
        protected override void Awake()
        {
            base.Awake();
            actions.Add("OpenItemShop", () => { UIManager.Instance.itemShopPanel.BuildPanel(goods); });
            dialogueConfig =
                Resources.LoadAsync("Config/Dialogue/DialogueConfig_NPC_Grocer_01").asset as DialogueConfig;
        }
    }
}