using System;
using System.Collections.Generic;

namespace Data
{

    [Serializable]
    public class CharacterStats
    {
        public Dictionary<StatType, CharacterStat> PrimaryStats = new Dictionary<StatType, CharacterStat>();
        public Dictionary<DamageType,DamageResistance> Resistances = new Dictionary<DamageType, DamageResistance>();
        public List<EquipmentSlotType> EquipmentSlots;

        public CharacterStats(KeyValuePair<StatType, float>[] stats, KeyValuePair<DamageType, DamageResistance>[] resistances = null, EquipmentSlotType[] equipmentSlots = null)
        {
            foreach (var pair in stats)
            {
                PrimaryStats.Add(pair.Key, new CharacterStat(pair.Value));
            }
            if (resistances != null)
            {
                foreach (var pair in resistances)
                {
                    Resistances.Add(pair.Key, pair.Value);
                }
            }
            EquipmentSlots = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();
        }
    }
}
