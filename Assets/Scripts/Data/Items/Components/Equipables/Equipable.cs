using System.Collections.Generic;

namespace Data.Items
{
    public class Equipable : IEquipable
    {
        public Equipable(IEquipableStats equipableStats, IItem itemParent)
        {
            SlotType = equipableStats.SlotType;
            Modifiers = equipableStats.Modifiers;
            Item = itemParent;
        }

        public EquipmentSlotType SlotType { get; protected set; }

        public IList<StatModifier> Modifiers { get ; }

        public IItem Item { get; }
    }
}
