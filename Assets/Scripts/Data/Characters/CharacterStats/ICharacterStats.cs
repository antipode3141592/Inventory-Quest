using Data.Items;
using System.Collections.Generic;

namespace Data.Characters
{
    public interface ICharacterStats
    {

        string Id { get; }
        string Name { get; }
        string DisplayName { get; set; }
        string PortraitPath { get; }

        string SpeciesId { get; }


        Strength Strength { get; set; }
        Vitality Vitality { get; set; }
        Agility Agility { get; set; }
        Charisma Charisma { get; set; }
        Intellect Intellect { get; set; }
        Speed Speed { get; set; }
        Spirit Spirit { get; set; }
        Arcane Arcane { get; set; }


        Attack Attack { get; set; }
        Defense Defense { get; set; }
        Initiative Initiative { get; set; }

        int MaximumHealth { get; }
        int MaximumMagicPool { get; }
        int CurrentHealth { get; set; }
        int CurrentMagicPool { get; set; }

        
        IDictionary<CharacterStatTypes, IStat> StatDictionary { get; }
        IDictionary<DamageType, DamageResistance> Resistances { get; }
        IList<EquipmentSlotType> EquipmentSlotsTypes { get; }

        
        
        
    }
}