using App.Enviorment;
using UnityEngine;

namespace App.UI
{
    public class HUDPanel : MonoBehaviour
    {
        public HUDBar hpBar { get; set; }
        public HUDBar mpBar { get; set; }
        public HUDBar expBar { get; set; }

        void Awake()
        {
            hpBar = transform.GetChild(0).GetComponent<HUDBar>();
            mpBar = transform.GetChild(1).GetComponent<HUDBar>();
            expBar = transform.GetChild(2).GetComponent<HUDBar>();
        }

        public void UpdatePanel()
        {
            UIManager.Instance.hudPanel.hpBar.UpdateBar(new Vector3(GameManager.Instance.player.currentHP / GameManager.Instance.player.professionAttribute.hp, 1, 1));
            UIManager.Instance.hudPanel.mpBar.UpdateBar(new Vector3(GameManager.Instance.player.currentMP / GameManager.Instance.player.professionAttribute.mp, 1, 1));
            UIManager.Instance.hudPanel.expBar.UpdateBar(new Vector3(GameManager.Instance.player.currentEXP / GameManager.Instance.player.professionAttribute.exp, 1, 1));
        }
    }
}
