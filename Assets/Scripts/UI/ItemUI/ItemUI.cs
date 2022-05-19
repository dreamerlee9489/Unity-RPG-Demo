using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using App.Manager;
using App.Items;

namespace App.UI
{
    [System.Serializable]
    public class ItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        Transform originParent = null;
        public GameItem item { get; set; }
        protected void UseItem() { item.Use(GameManager.Instance.player); }

        Transform CheckSlotType(GameObject obj)
        {
            if (obj == null)
                return originParent;
            ItemSlot originSlot = originParent.GetComponent<ItemSlot>();
            ItemSlot targetSlot = obj.GetComponent<ItemSlot>();
            if (targetSlot == null)
            {
                ItemUI target = obj.GetComponent<ItemUI>();
                if (target == null)
                    return originParent;
                else
                {
                    targetSlot = target.transform.parent.GetComponent<ItemSlot>();
                    if (originSlot.slotType == targetSlot.slotType)
                        return SwapItemUI(targetSlot, target);
                    else if (targetSlot.slotType == SlotType.ACTION)
                    {
                        if (targetSlot.itemUI.item.config.itemType == ItemType.SKILL)
                            return originParent;
                        if (item.containerType == ContainerType.EQUIPMENT)
                        {
                            if (item.config.itemType != targetSlot.itemUI.item.config.itemType)
                                return originParent;
                            GameManager.Instance.player.DetachEquipment(item as Equipment);
                            GameManager.Instance.player.AttachEquipment(targetSlot.itemUI.item as Equipment);
                            targetSlot.itemUI.item.containerType = ContainerType.EQUIPMENT;
                        }
                        item.containerType = ContainerType.ACTION;
                        return SwapItemUI(targetSlot, target);
                    }
                    else if (targetSlot.slotType == SlotType.BAG)
                    {
                        if (item.config.itemType == ItemType.SKILL)
                            return originParent;
                        if (item.containerType == ContainerType.EQUIPMENT)
                        {
                            if (item.config.itemType != targetSlot.itemUI.item.config.itemType)
                                return originParent;
                            GameManager.Instance.player.DetachEquipment(item as Equipment);
                            GameManager.Instance.player.AttachEquipment(targetSlot.itemUI.item as Equipment);
                            targetSlot.itemUI.item.containerType = ContainerType.EQUIPMENT;
                        }
                        item.containerType = ContainerType.BAG;
                        return SwapItemUI(targetSlot, target);
                    }
                    else
                    {
                        if (item.config.itemType != targetSlot.itemType)
                            return originParent;
                        if (item.containerType == ContainerType.EQUIPMENT)
                        {
                            GameManager.Instance.player.DetachEquipment(item as Equipment);
                            GameManager.Instance.player.AttachEquipment(targetSlot.itemUI.item as Equipment);
                            item.containerType = targetSlot.itemUI.item.containerType;
                            targetSlot.itemUI.item.containerType = ContainerType.EQUIPMENT;
                        }
                        else
                        {
                            GameManager.Instance.player.DetachEquipment(targetSlot.itemUI.item as Equipment);
                            GameManager.Instance.player.AttachEquipment(item as Equipment);
                            targetSlot.itemUI.item.containerType = item.containerType;
                            item.containerType = ContainerType.EQUIPMENT;
                        }
                        return SwapItemUI(targetSlot, target);
                    }
                }
            }
            else
            {
                if (originSlot.slotType == targetSlot.slotType)
                    return targetSlot.transform;
                else if (targetSlot.slotType == SlotType.ACTION)
                {
                    if (item.containerType == ContainerType.EQUIPMENT)
                        GameManager.Instance.player.DetachEquipment(item as Equipment);
                    item.containerType = ContainerType.ACTION;
                    return targetSlot.transform;
                }
                else if (targetSlot.slotType == SlotType.BAG)
                {
                    if (item.config.itemType == ItemType.SKILL)
                        return originParent;
                    if (item.containerType == ContainerType.EQUIPMENT)
                        GameManager.Instance.player.DetachEquipment(item as Equipment);
                    item.containerType = ContainerType.BAG;
                    return targetSlot.transform;
                }
                else
                {
                    if (item.config.itemType != targetSlot.itemType)
                        return originParent;
                    GameManager.Instance.player.AttachEquipment(item as Equipment);
                    item.containerType = ContainerType.EQUIPMENT;
                    return targetSlot.transform;
                }
            }
        }

        Transform SwapItemUI(ItemSlot targetSlot, ItemUI target)
        {
            Transform temp = targetSlot.transform;
            target.transform.SetParent(originParent);
            target.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            return temp;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            originParent = transform.parent.parent;
            eventData.pointerDrag.transform.SetParent(UIManager.Instance.transform);
            GetComponent<Image>().raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            originParent.GetComponent<ItemSlot>().itemUI = null;
            eventData.pointerDrag.transform.SetParent(CheckSlotType(eventData.pointerEnter).GetComponent<ItemSlot>().icons);
            transform.parent.parent.GetComponent<ItemSlot>().itemUI = this;
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
