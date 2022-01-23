using System;

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

        public bool TryEquip(out EquipableItem previousItem, EquipableItem itemToEquip)
        {
            previousItem = null;
            if (itemToEquip.SlotType == EquipmentSlotType.All || itemToEquip.SlotType == SlotType)
            {
                if (EquippedItem == null)
                {
                    EquippedItem = itemToEquip;
                }
                else
                {
                    previousItem = Unequip();
                    EquippedItem = itemToEquip;
                }
                OnEquip?.Invoke(this, new ModifierEventArgs(itemToEquip.Modifiers));
                return true;
            }
            return false;
        }

        public EquipableItem Unequip()
        {
            var item = EquippedItem;
            EquippedItem = null;
            OnUnequip?.Invoke(this, new ModifierEventArgs(item.Modifiers));
            return item;
        }
    }
}
