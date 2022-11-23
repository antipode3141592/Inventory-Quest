using Data.Items;
using Data.Health;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    public interface ICharacterStats
    {
        public string Id { get; }
        public string Name { get; }
        public Sprite Portrait { get; }
        public string SpeciesId { get; }

        public IList<IWeaponProficiency> WeaponProficiencies { get; }
        public ISpeciesBaseStats SpeciesBaseStats { get; }
        public IDictionary<StatTypes, int> InitialStats { get; }
        public IDictionary<DamageType, DamageResistance> Resistances { get; }
        public IList<EquipmentSlotType> EquipmentSlotsTypes { get; }


    }
}