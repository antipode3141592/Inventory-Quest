using System.Collections.Generic;

namespace Data.Stats

{
    public interface IEquipableStats
    {
        public List<StatModifier> Modifiers { get; set; }

        public EquipmentSlotType SlotType { get; set; }
    }
}
