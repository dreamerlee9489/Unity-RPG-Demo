using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Manager;

namespace App.UI
{
    public class SkillPanel : BasePanel
    {
        int totalPrice = 0;
        Transform content = null;
        Button btnQuit = null;
        Button btnTrade = null;
        List<SkillBar> skillBars = new List<SkillBar>();
        SkillBar skillBar { get; set; }
        public Text total { get; set; }
        public Transform goods { get; set; }

        void Awake()
        {
            content = gameObject.GetComponentInChildren<ScrollRect>().content;
            skillBar = Resources.Load<SkillBar>("UI/SkillBar");
            total = transform.GetChild(3).GetChild(0).GetComponent<Text>();
            btnQuit = transform.GetChild(1).GetComponent<Button>();
            btnTrade = transform.GetChild(4).GetComponent<Button>();
            btnQuit.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
            btnTrade.onClick.AddListener(() =>
            {
                if (InventoryManager.Instance.playerData.golds >= totalPrice)
                {
                    for (int i = 0; i < skillBars.Count; i++)
                    {
                        if (skillBars[i].quantity > 0)
                        {
                            goods.GetChild(i).GetComponent<Item>().AddToInventory();
                            UIManager.Instance.messagePanel.ShowMessage("[系统]  你学习了技能：" + skillBars[i].item.itemConfig.itemName + " * " + skillBars[i].quantity, Color.yellow);
                        }
                        skillBars[i].quantity = 0;
                        skillBars[i].quantityText.text = "0";
                    }
                    InventoryManager.Instance.playerData.golds -= totalPrice;
                    UIManager.Instance.goldPanel.UpdatePanel();
                    totalPrice = 0;
                    total.text = "0";
                }
                else
                {
                    UIManager.Instance.messagePanel.ShowMessage("[系统]  余额不足，无法购买。", Color.red);
                }
            });
        }

        void ClearPanel()
        {
            if (skillBars.Count > 0)
            {
                foreach (var bar in skillBars)
                    if (bar != null)
                        Destroy(bar.gameObject);
                skillBars.Clear();
            }
        }

        public void BuildPanel(Transform goods)
        {
            ClearPanel();
            this.goods = goods;
            gameObject.SetActive(true);
            for (int i = 0; i < this.goods.childCount; i++)
            {
                Item item = this.goods.GetChild(i).GetComponent<Item>();
                skillBars.Add(Instantiate(skillBar, content));
                skillBars[skillBars.Count - 1].BuildBar(item);
            }
        }

        public void CountTotalPrice()
        {
            totalPrice = 0;
            for (int i = 0; i < skillBars.Count; i++)
                totalPrice += skillBars[i].totalPrice;
            total.text = totalPrice.ToString();
        }
    }
}
