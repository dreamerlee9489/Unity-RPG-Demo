using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.Manager;
using App.Item;
using App.Control;

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
                        if (targetUISlot.itemType == ItemType.SKILL)
                            return originParent;
                        if(item.panelType == PanelType.EQUIPMENT)
                        {
                            if(item.itemType != targetUISlot.itemType)
                                return originParent;
                            GameManager.Instance.entities["Player"].GetComponent<CombatEntity>().DetachEquipment();
                            item.panelType = PanelType.ACTION;
                            targetUISlot.itemUI.item.Use(GameManager.Instance.entities["Player"].transform);
                        }
                        Transform temp = targetUISlot.transform;
                        targetUI.transform.SetParent(originParent);
                        targetUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        item.panelType = PanelType.ACTION;
                        return temp;
                    }
                    else if (targetUISlot.slotType == SlotType.BAG)
                    {
                        if (item.itemType == ItemType.SKILL)
                            return originParent;
                        if(item.panelType == PanelType.EQUIPMENT)
                        {
                            if(item.itemType != targetUISlot.itemType)
                                return originParent;
                            GameManager.Instance.entities["Player"].GetComponent<CombatEntity>().DetachEquipment();
                            item.panelType = PanelType.ACTION;
                            targetUISlot.itemUI.item.Use(GameManager.Instance.entities["Player"].transform);
                        }
                        Transform temp = targetUISlot.transform;
                        targetUI.transform.SetParent(originParent);
                        targetUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        item.panelType = PanelType.BAG;
                        return temp;
                    }
                    else
                    {
                        if (item.itemType == targetSlot.itemType)
                        {
                            Transform temp = targetUISlot.transform;
                            targetUI.transform.SetParent(originParent);
                            targetUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            item.panelType = PanelType.EQUIPMENT;
                            return temp;
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
                    if (item.panelType == PanelType.EQUIPMENT)
                        GameManager.Instance.entities["Player"].GetComponent<CombatEntity>().DetachEquipment();
                    item.panelType = PanelType.ACTION;
                    return targetSlot.transform;
                }
                else if (targetSlot.slotType == SlotType.BAG)
                {
                    if (item.itemType == ItemType.SKILL)
                        return originParent;
                    if (item.panelType == PanelType.EQUIPMENT)
                        GameManager.Instance.entities["Player"].GetComponent<CombatEntity>().DetachEquipment();
                    item.panelType = PanelType.BAG;
                    return targetSlot.transform;
                }
                else
                {
                    if (item.itemType != targetSlot.itemType)
                        return originParent;
                    item.Use(GameManager.Instance.entities["Player"].transform);
                    return targetSlot.transform;
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
