using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.Manager;

namespace App.UI
{
    public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [HideInInspector] public Transform originParent = null;
        public ItemType itemType = ItemType.ALL;

        protected virtual void UseItem() { print(" Use: " + gameObject.name); }

        Transform CheckSlotType(GameObject obj)
        {
            if (obj != null)
            {
                ItemSlot slot = obj.GetComponent<ItemSlot>();
                if (slot != null)
                {
                    if (slot.itemType == ItemType.ALL || slot.itemType == itemType)
                        return slot.transform;
                }
                else
                {
                    ItemUI other = obj.GetComponent<ItemUI>();
                    if (other != null)
                    {
                        ItemSlot itemSlot = originParent.GetComponent<ItemSlot>();
                        ItemSlot otherSlot = other.transform.parent.GetComponent<ItemSlot>();
                        if (itemSlot.itemType == otherSlot.itemType)
                        {
                            Transform temp = other.transform.parent;
                            other.transform.SetParent(originParent);
                            other.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            originParent = temp;
                        }
                    }
                    return originParent;
                }
            }
            return originParent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originParent = transform.parent;
            transform.position = eventData.position;
            eventData.pointerDrag.transform.SetParent(GameManager.Instance.canvas.transform);
            GetComponent<Image>().raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
            eventData.pointerDrag.transform.SetParent(CheckSlotType(eventData.pointerEnter));
            GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            GetComponent<Image>().raycastTarget = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerId == -2)
                UseItem();
        }
    }
}
