using Data.Items;
using System.Collections.Generic;

namespace Data.Characters
{
    public interface ICharacterStats
    {
        public string Id { get; }
        public string Name { get; }
        public string PortraitPath { get; }
        public string SpeciesId { get; }

        public IList<IWeaponProficiency> WeaponProficiencies { get; }
        public ISpeciesBaseStats SpeciesBaseStats { get; }
        public IDictionary<StatTypes, int> InitialStats { get; }
        public IDictionary<DamageType, DamageResistance> Resistances { get; }
        public IList<EquipmentSlotType> EquipmentSlotsTypes { get; }
    }
}