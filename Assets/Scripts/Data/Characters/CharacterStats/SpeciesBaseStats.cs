using Data.Items;
using System.Collections.Generic;

namespace Data.Characters
{
    public class SpeciesBaseStats : ISpeciesBaseStats
    {
        readonly string id;
        readonly Dictionary<StatTypes, int> baseStats;
        readonly List<EquipmentSlotType> slotTypes;

        public string Id => id;
        public Dictionary<StatTypes, int> BaseStats => baseStats;
        public List<EquipmentSlotType> SlotTypes => slotTypes;

        public SpeciesBaseStats(string id, Dictionary<StatTypes, int> baseStats, List<EquipmentSlotType> slotTypes)
        {
            this.id = id;
            this.baseStats = baseStats;
            this.slotTypes = slotTypes;
        }
    }
}
