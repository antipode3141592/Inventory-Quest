using Data.Health;
using Data.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{

    [Serializable]
    public class CharacterStats : ICharacterStats
    {
        public string Id { get; }
        public string Name { get; }
        public Sprite Portrait { get; }
        public string SpeciesId { get; }
        public ISpeciesBaseStats SpeciesBaseStats { get; }


        public IDictionary<StatTypes, int> InitialStats { get; }
        public IDictionary<DamageType, DamageResistance> Resistances { get; } = new Dictionary<DamageType, DamageResistance>();
        public IList<EquipmentSlotType> EquipmentSlotsTypes { get; }

        public IList<IWeaponProficiency> WeaponProficiencies { get; }

        public List<IItemStats> StartingEquipment { get; }

        public List<IItemStats> StartingInventory { get; }

        public CharacterStats(
            string name,
            string id,
            Sprite portrait,
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
            Portrait = portrait;

            Resistances = resistances is not null ? resistances : new();
            EquipmentSlotsTypes = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();
            WeaponProficiencies = weaponProficiencies is not null ? weaponProficiencies : new List<IWeaponProficiency>();

            InitialStats = initialStats;
        }


    }
}
