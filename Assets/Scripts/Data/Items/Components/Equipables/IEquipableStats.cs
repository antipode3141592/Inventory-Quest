using System.Collections.Generic;

namespace Data.Items

{
    public interface IEquipableStats : IItemComponentStats
    {
        public List<StatModifier> Modifiers { get; }

        public EquipmentSlotType SlotType { get; }
    }
}
