using Data.Items;
using System;

namespace Data.Characters
{
    public class EquipmentSlot
    {
        public EquipmentSlotType SlotType { get; }

        public IEquipable EquippedItem { get; set; }

        public EquipmentSlot(EquipmentSlotType slotType)
        {
            SlotType = slotType;
        }

        public EventHandler<ModifierEventArgs> OnEquip;
        public EventHandler<ModifierEventArgs> OnUnequip;

        public bool TryEquip(out IEquipable previousItem, IEquipable item)
        {
            previousItem = null;
            if (item is null) return false;
            if (IsValidPlacement(item))
            {
                TryUnequip(out previousItem);
                EquippedItem = item;
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

        public bool IsValidPlacement(IEquipable item)
        {
            return item.SlotType == SlotType;
        }
    }
}
