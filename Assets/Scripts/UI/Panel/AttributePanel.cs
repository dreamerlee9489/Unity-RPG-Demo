using UnityEngine.UI;
using App.Manager;
using App.Control;

namespace App.UI
{
    public class AttributePanel : BasePanel
    {
        Text lv = null, hp = null, mp = null, atk = null, def = null, exp = null;

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
			UpdatePanel();
        }

        public void UpdatePanel()
        {
            lv.text = GameManager.Instance.player.level.ToString();
            hp.text = (int)GameManager.Instance.player.currentHP + " / " + (int)GameManager.Instance.player.maxHP;
            mp.text = (int)GameManager.Instance.player.currentMP + " / " + (int)GameManager.Instance.player.maxMP;
            exp.text = (int)GameManager.Instance.player.currentEXP + " / " + (int)GameManager.Instance.player.maxEXP;
            atk.text = GameManager.Instance.player.currentATK.ToString();
            def.text = GameManager.Instance.player.currentDEF.ToString();
        }
    }
}
