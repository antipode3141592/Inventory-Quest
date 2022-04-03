using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IEquipableStats
    {
        public List<StatModifier> Modifiers { get; set; }

        public EquipmentSlotType SlotType { get; set; }
    }
}
