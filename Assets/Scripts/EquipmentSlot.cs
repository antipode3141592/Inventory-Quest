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
                    previousItem = EquippedItem;
                    EquippedItem = itemToEquip;
                }
                return true;
            }
            return false;
        }

        public EquipableItem Unequip()
        {
            var item = EquippedItem;
            EquippedItem = null;
            return item;
        }
    }
}
