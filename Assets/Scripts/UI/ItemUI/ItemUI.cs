using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.Manager;
using App.Item;

namespace App.UI
{
    [System.Serializable]
    public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        Transform originParent = null;
        public GameItem item { get; set; }

        protected void UseItem() { item.Use(GameManager.Instance.entities["Player"].transform); }

        Transform CheckSlotType(GameObject target)
        {
            if (target == null)
                return originParent;
            ItemSlot originSlot = originParent.GetComponent<ItemSlot>();
            ItemSlot targetSlot = target.GetComponent<ItemSlot>();
            if (targetSlot == null)
            {
                ItemUI targetUI = target.GetComponent<ItemUI>();
                if (targetUI == null)
                    return originParent;
                else
                {
                    ItemSlot targetUISlot = targetUI.transform.parent.GetComponent<ItemSlot>();
                    if (originSlot.slotType == targetUISlot.slotType)
                    {
                        Transform temp = targetUISlot.transform;
                        targetUI.transform.SetParent(originParent);
                        targetUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        return temp;
                    }
                    else if (targetUISlot.slotType == SlotType.ACTION)
                    {
                        if (targetUISlot.itemType != ItemType.SKILL)
                        {
                            Transform temp = targetUISlot.transform;
                            targetUI.transform.SetParent(originParent);
                            targetUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            item.panelType = PanelType.ACTION;
                            return temp;
                        }
                        return originParent;
                    }
                    else if (targetUISlot.slotType == SlotType.BAG)
                    {
                        if (item.itemType != ItemType.SKILL)
                        {
                            Transform temp = targetUISlot.transform;
                            targetUI.transform.SetParent(originParent);
                            targetUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            item.panelType = PanelType.BAG;
                            return temp;
                        }
                        return originParent;
                    }
                    else
                    {
                        switch (item.itemType)
                        {
                            case ItemType.ARMOR:
                            case ItemType.BOOTS:
                            case ItemType.BRACER:
                            case ItemType.HELMET:
                            case ItemType.NECKLACE:
                            case ItemType.PANTS:
                            case ItemType.SHIELD:
                            case ItemType.WEAPON:
                                if (item.itemType == targetSlot.itemType)
                                {
                                    item.panelType = PanelType.EQUIPMENT;
                                    return targetSlot.transform;
                                }
                                break;
                            case ItemType.POTION:
                            case ItemType.SKILL:
                                break;
                        }
                        return originParent;
                    }
                }
            }
            else
            {
                if (originSlot.slotType == targetSlot.slotType)
                    return targetSlot.transform;
                else if (targetSlot.slotType == SlotType.ACTION)
                {
                    item.panelType = PanelType.ACTION;
                    return targetSlot.transform;
                }
                else if (targetSlot.slotType == SlotType.BAG)
                {
                    if (item.itemType != ItemType.SKILL)
                    {
                        item.panelType = PanelType.BAG;
                        return targetSlot.transform;
                    }
                    return originParent;
                }
                else
                {
                    switch (item.itemType)
                    {
                        case ItemType.ARMOR:
                        case ItemType.BOOTS:
                        case ItemType.BRACER:
                        case ItemType.HELMET:
                        case ItemType.NECKLACE:
                        case ItemType.PANTS:
                        case ItemType.SHIELD:
                        case ItemType.WEAPON:
                            if (item.itemType == targetSlot.itemType)
                            {
                                item.panelType = PanelType.EQUIPMENT;
                                return targetSlot.transform;
                            }
                            break;
                        case ItemType.POTION:
                        case ItemType.SKILL:
                            break;
                    }
                    return originParent;
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originParent = transform.parent;
            eventData.pointerDrag.transform.SetParent(GameManager.Instance.canvas.transform);
            GetComponent<Image>().raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
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
