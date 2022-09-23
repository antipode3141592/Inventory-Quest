using Data.Items;
using System.Collections.Generic;

namespace Data.Characters
{
    public interface ISpeciesBaseStats
    {
        string Id { get; }
        Dictionary<StatTypes, int> BaseStats { get; }
        List<EquipmentSlotType> SlotTypes { get; }
    }
}