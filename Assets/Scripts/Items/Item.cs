﻿using UnityEngine;
using App.Config;
using App.Control;
using App.UI;
using App.Manager;

namespace App.Items
{
    public enum ItemType { NONE, HELMET, BREAST, SHIELD, BOOTS, NECKLACE, HAND, WEAPON, PANTS, POTION, SKILL }
    public enum EquipmentType { WEAPON, ARMOR, JEWELRY }
    public enum ContainerType { WORLD, ENTITY, BAG, EQUIPMENT, ACTION }

    [RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
    public abstract class Item : MonoBehaviour
    {
        NameBar nameBar = null;
        public ItemConfig itemConfig = null;
        public ContainerType containerType = ContainerType.WORLD;
        public ItemUI itemUI { get; set; }
        public new Collider collider { get; set; }
        public new Rigidbody rigidbody { get; set; }
        public abstract void Use(CombatEntity user);
        public abstract void AddToInventory();
        public override bool Equals(object other) => itemConfig == (other as Item).itemConfig;
        public override int GetHashCode() => itemConfig.itemName.GetHashCode();

        protected void Awake()
        {
            collider = GetComponent<Collider>();
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            collider.enabled = containerType == ContainerType.WORLD ? true : false;
            rigidbody.useGravity = containerType == ContainerType.WORLD ? true : false;
        }

        void Update()
        {
            if (containerType == ContainerType.WORLD)
            {
                if(nameBar == null)
                {
                    nameBar = Instantiate(Resources.Load<NameBar>("UI/NameBar"));
                    nameBar.text.text = itemConfig.itemName;
                }
                nameBar.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                for (int i = 0; i < GameManager.Instance.registeredQuests.Count; i++)
                {
                    Item item = GameManager.Instance.registeredQuests[i].target.GetComponent<Item>();
                    if (item != null && Equals(item))
                        GameManager.Instance.registeredQuests[i].UpdateProgress(1);
                }
                UIManager.Instance.messagePanel.ShowMessage("[系统]  你拾取了" + itemConfig.itemName + " * 1");
                AddToInventory();
                if(nameBar != null)
                {
                    Destroy(nameBar.gameObject);
                    nameBar = null;
                }
                Destroy(gameObject);
            }
        }
    }
}
