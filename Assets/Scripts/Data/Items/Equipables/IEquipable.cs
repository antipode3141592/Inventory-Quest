using System.Collections.Generic;

namespace Data.Items

{
    public interface IEquipable
    {
        public EquipmentSlotType SlotType { get; }
        public IList<StatModifier> Modifiers { get; set; }

    }
}
