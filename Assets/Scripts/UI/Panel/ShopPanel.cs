using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.UI
{
    public enum ShopType { ITEM, SKILL }

    public abstract class ShopPanel : BasePanel
    {
        protected int total = 0;
        protected Transform content = null;
        protected Button btnQuit = null;
        protected Button btnTrade = null;
        protected ShopBar shopBarPrefab = null;
        protected List<ShopBar> shopBars = new List<ShopBar>();
        public ShopType shopType = ShopType.ITEM;
        public Text totalText { get; set; }
        public Text hint { get; set; }
        public Transform goods { get; set; }

        protected virtual void Awake()
        {
            content = gameObject.GetComponentInChildren<ScrollRect>().content;
            btnQuit = transform.GetChild(1).GetComponent<Button>();
            btnTrade = transform.GetChild(2).GetComponent<Button>();
            totalText = transform.GetChild(3).GetChild(0).GetComponent<Text>();
            hint = transform.GetChild(4).GetComponent<Text>();
            hint.text = "";
            btnQuit.onClick.AddListener(() => { gameObject.SetActive(false); });
        }

        public virtual void BuildPanel(Transform goods)
        {
            ClearPanel();
            this.goods = goods;
            gameObject.SetActive(true);
        }

        protected void ClearPanel()
        {
            if (shopBars.Count > 0)
            {
                foreach (var bar in shopBars)
                    if (bar != null)
                        Destroy(bar.gameObject);
                shopBars.Clear();
            }
        }

        public void CountTotalPrice()
        {
            total = 0;
            for (int i = 0; i < shopBars.Count; i++)
                total += shopBars[i].total;
            totalText.text = total.ToString();
        }
    }
}
