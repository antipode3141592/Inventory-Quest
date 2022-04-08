using Data.Stats;
using System.Collections.Generic;

namespace Data.Interfaces

{
    public interface IEquipable
    {
        public EquipmentSlotType SlotType { get; }
        public IList<StatModifier> Modifiers { get; set; }

    }
}
