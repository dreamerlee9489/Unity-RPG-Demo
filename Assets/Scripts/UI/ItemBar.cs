using UnityEngine;
using UnityEngine.UI;
using App.Items;
using App.Manager;
using UnityEngine.EventSystems;

namespace App.UI
{
	public class ItemBar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		Image itemIcon = null;
		Text itemName = null; 
		Text priceText = null;
		Button btnMinus = null;
		Button btnPlus = null;
		public int count { get; set; }
		public int quantity { get; set; }
		public int unitPrice { get; set; }
		public int totalPrice { get; set; }
		public Text quantityText { get; set; }
		public Item item { get; set; }

		void Awake()
		{
			count = UIManager.Instance.shopPanel.isSell ? 1 : 10;
			itemIcon = transform.GetChild(0).GetComponent<Image>();
			itemName = transform.GetChild(1).GetComponent<Text>();
			priceText = transform.GetChild(3).GetComponent<Text>();
			btnMinus = transform.GetChild(4).GetComponent<Button>();
			quantityText = transform.GetChild(5).GetComponent<Text>();
			btnPlus = transform.GetChild(6).GetComponent<Button>();
			quantityText.text = "0";
			btnMinus.onClick.AddListener(() => {
				quantity = Mathf.Max(--quantity, 0);
				totalPrice = quantity * unitPrice;  
				quantityText.text = quantity.ToString();
				UIManager.Instance.shopPanel.CountTotalPrice();
			});
			btnPlus.onClick.AddListener(() => {
				quantity = Mathf.Min(++quantity, count);
				totalPrice = quantity * unitPrice;  
				quantityText.text = quantity.ToString();
				UIManager.Instance.shopPanel.CountTotalPrice();
			});
		}

		public void BuildBar(Item item)
		{
			this.item = item;
			itemIcon.sprite = item.itemConfig.itemUI.GetComponent<Image>().sprite;
			itemName.text = item.itemConfig.itemName;
			unitPrice = (int)(item.itemConfig.price * (UIManager.Instance.shopPanel.isSell ? 0.5f : 1f)); 
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
