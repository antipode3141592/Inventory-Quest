using System.Collections.Generic;

namespace Data.Interfaces

{
    public interface IEquipable
    {
        public EquipmentSlotType SlotType { get; }
        public List<StatModifier> Modifiers { get; set; }

    }
}
