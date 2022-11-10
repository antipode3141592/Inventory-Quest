using Data.Items;
using System;
using UnityEngine;

namespace Data.Characters
{
    public class EquipmentSlot
    {
        public string Id { get; }

        public EquipmentSlotType SlotType { get; }

        public IItem EquippedItem { get; set; }

        public EquipmentSlot(EquipmentSlotType slotType, string id = null)
        {
            SlotType = slotType;
            Id = id is null ? $"{slotType}" : id;
        }

        public EventHandler<ModifierEventArgs> OnEquip;
        public EventHandler<ModifierEventArgs> OnUnequip;

        public bool TryEquip(out IItem previousItem, IItem item)
        {
            previousItem = null;
            if (item is null)
                return false;
            IEquipable equipable = item.Components[typeof(IEquipable)] as IEquipable;
            if (equipable is null)
            {
                Debug.Log($"could not find equipable component of item {item.Id}");
                return false;
            }
            if (equipable.SlotType != SlotType)
            {
                Debug.Log($"item {item.Id} has slot type {EquipmentSlotTypeExtensions.PrettyPrintSlotType(equipable.SlotType)} which does not match {EquipmentSlotTypeExtensions.PrettyPrintSlotType(SlotType)}");
                return false;
            }
            Debug.Log($"{equipable.Item.Id} is an equipable component of item {item.Id}");
            if (TryUnequip(out previousItem))
            {
                Debug.Log($"TryUnequip success, output item {previousItem.Id}");
            }
            Debug.Log($"TryUnequip did not find an item to unequip");
            EquippedItem = item;
            Debug.Log($"EquippedItem = {EquippedItem.Id}");

            OnEquip?.Invoke(this, new ModifierEventArgs(equipable.Modifiers));
            return true;
        }

        public bool TryUnequip(out IItem item)
        {
            item = null;
            if (EquippedItem is null)
            {
                Debug.Log($"EquippedItem is Null, exiting TryUnequip");
                return false; 
            }
            item = EquippedItem;
            EquippedItem = null;
            IEquipable equipable = item.Components[typeof(IEquipable)] as IEquipable;
            if (equipable is null) return false;
            OnUnequip?.Invoke(this, new ModifierEventArgs(equipable.Modifiers));
            return true;
        }

        public bool IsValidPlacement(IItem item)
        {
            var equipable = (IEquipable)item.Components[typeof(IEquipable)];
            if (equipable is null) return false;

            Debug.Log($"{equipable.Item.Id} is a valid placement");
            return equipable.SlotType == SlotType;
        }
    }
}
