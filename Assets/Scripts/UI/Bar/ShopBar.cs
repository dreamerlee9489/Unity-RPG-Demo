using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Items;
using Manager;

namespace UI
{
    public class ShopBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        protected Image itemIcon = null;
        protected Text itemName = null;
        protected Text priceText = null;
        protected Button btnMinus = null;
        protected Button btnPlus = null;
        protected ShopPanel shopPanel = null;
        public int count { get; set; }
        public int price { get; set; }
        public int total { get; set; }
        public Text countText { get; set; }
        public Item shopItem { get; set; }

        protected virtual void Awake()
        {
            itemIcon = transform.GetChild(0).GetComponent<Image>();
            itemName = transform.GetChild(1).GetComponent<Text>();
            priceText = transform.GetChild(3).GetComponent<Text>();
            btnMinus = transform.GetChild(4).GetComponent<Button>();
            countText = transform.GetChild(5).GetComponent<Text>();
            btnPlus = transform.GetChild(6).GetComponent<Button>();
        }

        public virtual void BuildBar(Item item, ShopPanel shopPanel)
        {
            this.shopItem = item;
            this.shopPanel = shopPanel;
            itemIcon.sprite = item.itemConfig.itemUI.GetComponent<Image>().sprite;
            itemName.text = item.itemConfig.itemName;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 position = eventData.pointerEnter.transform.position;
            float panelWidth = UIManager.Instance.tipPanel.transform.GetComponent<RectTransform>().rect.width;
            UIManager.Instance.tipPanel.transform.position = position.x > panelWidth
                ? position
                : position + new Vector3(GetComponent<RectTransform>().rect.width + panelWidth, 0, 0);
            UIManager.Instance.tipPanel.Draw(shopItem);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.tipPanel.Erase();
        }
    }
}