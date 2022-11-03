using System.Collections.Generic;

namespace Data.Items

{
    public interface IEquipable : IItemComponent
    {
        public EquipmentSlotType SlotType { get; }
        public IList<StatModifier> Modifiers { get; }
    }
}
