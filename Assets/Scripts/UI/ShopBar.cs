using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.Items;
using App.Manager;
using App.SO;
using App.Control;

namespace App.UI
{
    public class ShopBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        Image itemIcon = null;
        Text itemName = null;
        Text priceText = null;
        Button btnMinus = null;
        Button btnPlus = null;
        ShopPanel shopPanel = null;
        public int inventory { get; set; }
        public int count { get; set; }
        public int unitPrice { get; set; }
        public int totalPrice { get; set; }
        public Text countText { get; set; }
        public Item item { get; set; }

        void Awake()
        {
            inventory = UIManager.Instance.itemShopPanel.isSell ? 1 : 10;
            itemIcon = transform.GetChild(0).GetComponent<Image>();
            itemName = transform.GetChild(1).GetComponent<Text>();
            priceText = transform.GetChild(3).GetComponent<Text>();
            btnMinus = transform.GetChild(4).GetComponent<Button>();
            countText = transform.GetChild(5).GetComponent<Text>();
            btnPlus = transform.GetChild(6).GetComponent<Button>();
            btnMinus.onClick.AddListener(() =>
            {
                count = Mathf.Max(--count, 0);
                totalPrice = count * unitPrice;
                countText.text = count.ToString();
                UIManager.Instance.itemShopPanel.CountTotalPrice();
            });
            btnPlus.onClick.AddListener(() =>
            {
                count = Mathf.Min(++count, inventory);
                totalPrice = count * unitPrice;
                countText.text = count.ToString();
                UIManager.Instance.itemShopPanel.CountTotalPrice();
            });
        }

        void Start()
        {
            inventory = GetInventory();
            countText.text = InitialCount().ToString();
        }

        int GetInventory()
        {
            if(shopPanel.shopType == ShopType.SKILL)
                return inventory = (item.itemConfig as SkillConfig).levelRequires.Count;
            return inventory;
        }

        int InitialCount()
        {
            if(shopPanel.shopType == ShopType.SKILL)
            {
                for (int i = 0; i < InventoryManager.Instance.skills.childCount; i++)
                {
                    Skill skill = InventoryManager.Instance.skills.GetChild(i).GetComponent<Skill>();
                    if (skill.Equals(item))
                        return count = skill.level + 1;
                }
            }
            return count = 0;
        }

        public void BuildBar(Item item, ShopPanel shopPanel)
        {
            this.item = item;
            this.shopPanel = shopPanel;
            itemIcon.sprite = item.itemConfig.itemUI.GetComponent<Image>().sprite;
            itemName.text = item.itemConfig.itemName;
            unitPrice = (int)(item.itemConfig.itemPrice * (UIManager.Instance.itemShopPanel.isSell ? 0.5f : 1f));
            priceText.text = unitPrice.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 position = eventData.pointerEnter.transform.position;
            float panelWidth = UIManager.Instance.tipPanel.transform.GetComponent<RectTransform>().rect.width;
            UIManager.Instance.tipPanel.transform.position = position.x > panelWidth ? position : position + new Vector3(GetComponent<RectTransform>().rect.width + panelWidth, 0, 0);
            UIManager.Instance.tipPanel.Draw(item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.tipPanel.Erase();
        }
    }
}
