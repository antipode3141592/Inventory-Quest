using System;
using System.Collections.Generic;

namespace Data.Stats
{

    [Serializable]
    public class CharacterStats
    {
        public string Name { get; }
        public string DisplayName { get; set; }

        public string PortraitPath { get; }

        public string SpeciesId { get; }

        public Strength Strength { get; set; }

        public Vitality Vitality { get; set; }
        
        public Agility Agility { get; set; }

        public Speed Speed { get; set; }

        public Charisma Charisma { get; set; }
        
        public Intellect Intellect { get; set; }
        public Spirit Spirit { get; set; }

        public Arcane Arcane { get; set; }


        //derived stats

        public Attack Attack { get; set; }
        public Defense Defense { get; set; }

        public Initiative Initiative { get; set; }


        public int HealthPerLevel { get; } = 5;
        public int MagicPerLevel { get; } = 5;
        public int CurrentHealth { get; set; }

        public int CurrentMagicPool { get; }
        public int MaximumHealth => Vitality.CurrentValue + (HealthPerLevel * CurrentLevel);
        public int MaximumMagicPool => Arcane.CurrentValue + Spirit.CurrentValue + ( MagicPerLevel * CurrentLevel);

        public int MaximumEncumbrance => Strength.CurrentValue * 15;

        public int CurrentExperience { get; set; }

        public int NextLevelExperience => (CurrentLevel^2) * 250 + CurrentLevel * 750;

        public int CurrentLevel { get; set; }


        public Dictionary<Type, IStat> Stats;
        public Dictionary<DamageType,DamageResistance> Resistances = new Dictionary<DamageType, DamageResistance>();
        public List<EquipmentSlotType> EquipmentSlotsTypes;

        public CharacterStats(
            string name, 
            string portraitPath, 
            string speciesId,
            Dictionary<Type,int> stats, 
            Dictionary<DamageType, DamageResistance> resistances = null, 
            EquipmentSlotType[] equipmentSlots = null)
        {
            Name = name;
            DisplayName = name;
            SpeciesId = speciesId;
            PortraitPath = portraitPath;

            Strength = new Strength(stats[typeof(Strength)]);
            Vitality = new Vitality(stats[typeof(Vitality)]);
            Agility = new Agility(stats[typeof(Agility)]);
            Speed = new Speed(stats[typeof(Speed)]);
            Charisma = new Charisma(stats[typeof(Charisma)]);
            Intellect = new Intellect(stats[typeof(Intellect)]);
            Spirit = new Spirit(stats[typeof(Spirit)]);
            Arcane = new Arcane(stats[typeof(Arcane)]);

            Attack = new Attack(0, new CharacterStat[] { Strength, Speed });
            Defense = new Defense(0, new CharacterStat[] { Agility, Vitality });
            Initiative = new Initiative(0, new CharacterStat[] { Agility, Speed });
            Resistances = resistances;

            EquipmentSlotsTypes = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();
            Stats = new Dictionary<Type, IStat>();
            var properties = typeof(CharacterStats).GetProperties();
            foreach(var property in properties)
            {
                if (typeof(IStat).IsAssignableFrom(property.PropertyType)) 
                    Stats.Add(property.PropertyType, (IStat)property.GetValue(this));

            }

            CurrentHealth = MaximumHealth;
            CurrentMagicPool = MaximumMagicPool;
        }


    }
}
