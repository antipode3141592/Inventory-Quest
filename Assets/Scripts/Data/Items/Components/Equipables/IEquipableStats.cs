using Data.Characters;
using System.Collections.Generic;

namespace Data.Items
{
    public interface IEquipableStats : IItemComponentStats
    {
        public List<StatModifier> StatModifiers { get; }
        public List<ResistanceModifier> ResistanceModifiers { get; }
        public EquipmentSlotType SlotType { get; }
    }
}
