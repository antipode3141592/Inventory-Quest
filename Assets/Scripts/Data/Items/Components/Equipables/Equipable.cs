using Data.Characters;
using System.Collections.Generic;

namespace Data.Items
{
    public class Equipable : IEquipable
    {
        public Equipable(IEquipableStats equipableStats, IItem itemParent)
        {
            SlotType = equipableStats.SlotType;
            StatModifiers = equipableStats.StatModifiers;
            ResistanceModifiers = equipableStats.ResistanceModifiers;
            Item = itemParent;
        }

        public EquipmentSlotType SlotType { get; protected set; }

        public IList<StatModifier> StatModifiers { get ; }

        public IItem Item { get; }

        public IList<ResistanceModifier> ResistanceModifiers { get; }
    }
}
