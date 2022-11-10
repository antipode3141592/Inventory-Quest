namespace Data.Items
{
    public static class EquipmentSlotTypeExtensions
    {
        public static string PrettyPrintSlotType(EquipmentSlotType slotType)
        {
            switch (slotType)
            { 
                case EquipmentSlotType.InnerTorso:
                    return "Inner Torso";
                case EquipmentSlotType.OuterTorso:
                    return "Outer Torso";
                case EquipmentSlotType.OneHandedWeapon:
                    return "One-Handed Weapon";
                case EquipmentSlotType.TwoHandedWeapon:
                    return "Two-Handed Weapon";
                case EquipmentSlotType.RangedWeapon:
                    return "Ranged Weapon";
                default: 
                    return slotType.ToString();
            }
        }
    }
}
