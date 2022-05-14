using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace App.Control
{
	public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		// RectTransform content = null;

        public void OnBeginDrag(PointerEventData eventData)
        {
			transform.position = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
			transform.position = eventData.position;
			// eventData.pointerDrag.transform.SetParent(null);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
			transform.position = eventData.position;
			// eventData.pointerDrag.transform.SetParent(content);
        }

		void Awake()
		{
			// content = GameManager.Instance.canvas.inventoryPanel.transform.GetChild(1).GetComponent<ScrollRect>().content;
		}
	}
}
