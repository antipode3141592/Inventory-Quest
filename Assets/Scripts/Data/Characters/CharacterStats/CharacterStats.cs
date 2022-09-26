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
        public ISpeciesBaseStats SpeciesBaseStats { get; }


        public IDictionary<StatTypes, int> InitialStats { get; }
        public IDictionary<DamageType, DamageResistance> Resistances { get; } = new Dictionary<DamageType, DamageResistance>();
        public IList<EquipmentSlotType> EquipmentSlotsTypes { get; }

        public IList<IWeaponProficiency> WeaponProficiencies { get; }

        public CharacterStats(
            string name,
            string id,
            string portraitPath,
            ISpeciesBaseStats species,
            Dictionary<StatTypes, int> initialStats,
            Dictionary<DamageType, DamageResistance> resistances = null,
            IList<EquipmentSlotType> equipmentSlots = null,
            IList<IWeaponProficiency> weaponProficiencies = null)
        {
            Id = id;
            Name = name;
            SpeciesBaseStats = species;
            SpeciesId = species.Id;
            PortraitPath = portraitPath;

            Resistances = resistances is not null ? resistances : new();
            EquipmentSlotsTypes = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();
            WeaponProficiencies = weaponProficiencies is not null ? weaponProficiencies : new List<IWeaponProficiency>();

            InitialStats = initialStats;
        }


    }
}
