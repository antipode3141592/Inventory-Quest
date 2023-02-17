using Data.Characters;
using System.Collections.Generic;

namespace Data.Items
{
    public interface IEquipable : IItemComponent
    {
        public EquipmentSlotType SlotType { get; }
        public IList<StatModifier> StatModifiers { get; }
        public IList<ResistanceModifier> ResistanceModifiers { get; }
    }
}
