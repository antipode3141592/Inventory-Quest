using Data.Items;
using System;

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
            if (equipable is null || equipable.SlotType != SlotType) return false;

            TryUnequip(out previousItem);
            EquippedItem = item;
            OnEquip?.Invoke(this, new ModifierEventArgs(equipable.Modifiers));
            return true;
        }

        public bool TryUnequip(out IItem item)
        {
            item = null;
            if (EquippedItem == null) return false;
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
            return equipable.SlotType == SlotType;
        }
    }
}
