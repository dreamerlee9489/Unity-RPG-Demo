using UnityEngine.UI;
using App.Manager;
using App.Control;

namespace App.UI
{
    public class AttributePanel : BasePanel
    {
        Text lv = null, hp = null, mp = null, atk = null, def = null, exp = null;
        CombatEntity player = null;

        void Awake()
        {
            lv = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
            hp = transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
            mp = transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();
            exp = transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>();
            atk = transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>();
            def = transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<Text>();
        }

        void Start()
        {
            player = GameManager.Instance.player;
			UpdatePanel();
        }

        public void UpdatePanel()
        {
            lv.text = player.level.ToString();
            hp.text = (int)player.currentHP + " / " + (int)player.maxHP;
            mp.text = (int)player.currentMP + " / " + (int)player.maxMP;
            exp.text = (int)player.currentEXP + " / " + (int)player.maxEXP;
            atk.text = player.currentATK.ToString();
            def.text = player.currentDEF.ToString();
        }
    }
}
