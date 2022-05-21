using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Manager;

namespace App.UI
{
    public enum GoodsType { ALL, WEAPON, ARMOR, JEWELRY, POTION }

    public class SelectBar : MonoBehaviour
    {
		public List<int> indexs = new List<int>();
        public Button btnAll = null;
        public Button btnWeapon = null;
        public Button btnArmor = null;
        public Button btnJewelry = null;
        public Button btnPotion = null;

        void Awake()
        {
            btnAll = transform.GetChild(0).GetComponent<Button>();
            btnWeapon = transform.GetChild(1).GetComponent<Button>();
            btnArmor = transform.GetChild(2).GetComponent<Button>();
            btnJewelry = transform.GetChild(3).GetComponent<Button>();
            btnPotion = transform.GetChild(4).GetComponent<Button>();
			btnAll.onClick.AddListener(() => {
				UIManager.Instance.shopPanel.ShowFiltered(GoodsFilter(UIManager.Instance.shopPanel.goods, GoodsType.ALL));
				UIManager.Instance.shopPanel.total.text = "0";
			});
			btnWeapon.onClick.AddListener(() => {
				UIManager.Instance.shopPanel.ShowFiltered(GoodsFilter(UIManager.Instance.shopPanel.goods, GoodsType.WEAPON));
				UIManager.Instance.shopPanel.total.text = "0";
			});
			btnArmor.onClick.AddListener(() => {
				UIManager.Instance.shopPanel.ShowFiltered(GoodsFilter(UIManager.Instance.shopPanel.goods, GoodsType.ARMOR));
				UIManager.Instance.shopPanel.total.text = "0";
			});
			btnJewelry.onClick.AddListener(() => {
				UIManager.Instance.shopPanel.ShowFiltered(GoodsFilter(UIManager.Instance.shopPanel.goods, GoodsType.JEWELRY));
				UIManager.Instance.shopPanel.total.text = "0";
			});
			btnPotion.onClick.AddListener(() => {
				UIManager.Instance.shopPanel.ShowFiltered(GoodsFilter(UIManager.Instance.shopPanel.goods, GoodsType.POTION));
				UIManager.Instance.shopPanel.total.text = "0";
			});
        }

        public List<int> GoodsFilter(Transform goods, GoodsType goodsType)
        {
			indexs.Clear();
            switch (goodsType)
            {
                case GoodsType.ALL:
                    for (int i = 0; i < goods.childCount; i++)
                        indexs.Add(i);
                    break;
                case GoodsType.ARMOR:
                    for (int i = 0; i < goods.childCount; i++)
                    {
                        ItemType itemType = goods.GetChild(i).GetComponent<Item>().itemConfig.itemType;
                        if (itemType == ItemType.BOOTS || itemType == ItemType.BREAST || itemType == ItemType.HELMET || itemType == ItemType.PANTS || itemType == ItemType.SHIELD)
                            indexs.Add(i);
                    }
                    break;
                case GoodsType.JEWELRY:
                    for (int i = 0; i < goods.childCount; i++)
                    {
                        ItemType itemType = goods.GetChild(i).GetComponent<Item>().itemConfig.itemType;
                        if (itemType == ItemType.BRACELET || itemType == ItemType.NECKLACE)
                            indexs.Add(i);
                    }
                    break;
                case GoodsType.WEAPON:
                    for (int i = 0; i < goods.childCount; i++)
                        if (goods.GetChild(i).GetComponent<Item>().itemConfig.itemType == ItemType.WEAPON)
                            indexs.Add(i);
                    break;
                case GoodsType.POTION:
                    for (int i = 0; i < goods.childCount; i++)
                        if (goods.GetChild(i).GetComponent<Item>().itemConfig.itemType == ItemType.POTION)
                            indexs.Add(i);
                    break;
            }
			return indexs;
        }
    }
}
