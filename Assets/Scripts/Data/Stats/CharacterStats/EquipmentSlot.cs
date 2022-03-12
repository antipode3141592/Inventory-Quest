using Data.Interfaces;
using System;
using UnityEngine;

namespace Data
{
    public class EquipmentSlot
    {
        public EquipmentSlotType SlotType { get; }

        public EquipableItem EquippedItem { get; set; }

        public EquipmentSlot(EquipmentSlotType slotType)
        {
            SlotType = slotType;
        }

        public EventHandler<ModifierEventArgs> OnEquip;
        public EventHandler<ModifierEventArgs> OnUnequip;

        public bool TryEquip(out IEquipable previousItem, IItem item)
        {
            previousItem = null;
            if (IsValidPlacement(item))
            {
                EquipableItem itemToEquip = item as EquipableItem;
                if (EquippedItem != null)
                {
                    Debug.Log($"{EquippedItem.Id} is equipped, unequipping...");
                    if (TryUnequip(out previousItem))
                        Debug.Log($"unequip successful");
                }
                EquippedItem = itemToEquip;
                OnEquip?.Invoke(this, new ModifierEventArgs(EquippedItem.Modifiers));
                return true;
            }
            return false;
        }

        public bool TryUnequip(out IEquipable item)
        {
            item = null;
            if (EquippedItem == null) return false;
            item = EquippedItem;
            EquippedItem = null;
            OnUnequip?.Invoke(this, new ModifierEventArgs(item.Modifiers));
            return true;
        }

        public bool IsValidPlacement(IItem item)
        {
            if (item is not EquipableItem equipableItem) return false;
            return equipableItem.SlotType == SlotType;
        }
    }
}
