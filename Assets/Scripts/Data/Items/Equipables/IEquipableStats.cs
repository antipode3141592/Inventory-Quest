using System.Collections.Generic;

namespace Data.Items

{
    public interface IEquipableStats
    {
        public List<StatModifier> Modifiers { get; set; }

        public EquipmentSlotType SlotType { get; set; }
    }
}
