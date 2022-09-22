using Data.Items;
using System;
using System.Collections.Generic;

namespace Data.Characters
{

    [Serializable]
    public class CharacterStats : ICharacterStats
    {
        public string Id { get; }
        public string Name { get; }
        public string PortraitPath { get; }
        public string SpeciesId { get; }

        public IDictionary<StatTypes, int> InitialStats { get; }
        public IDictionary<DamageType, DamageResistance> Resistances { get; } = new Dictionary<DamageType, DamageResistance>();
        public IList<EquipmentSlotType> EquipmentSlotsTypes { get; }

        public CharacterStats(
            string name,
            string id,
            string portraitPath,
            string speciesId,
            Dictionary<StatTypes, int> stats,
            Dictionary<DamageType, DamageResistance> resistances = null,
            IList<EquipmentSlotType> equipmentSlots = null)
        {
            Id = id;
            Name = name;
            SpeciesId = speciesId;
            PortraitPath = portraitPath;

            Resistances = resistances;
            EquipmentSlotsTypes = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();

            InitialStats = stats;
        }


    }
}
