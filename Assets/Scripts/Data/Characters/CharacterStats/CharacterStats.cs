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
        public string DisplayName { get; set; }
        public string PortraitPath { get; }
        public string SpeciesId { get; }

        //base stats
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
        public ArcaneAffinity ArcaneAffinity { get; set; }
        public SpiritAffinity SpiritAffinity { get; set; }
        public Initiative Initiative { get; set; }


        //skill stats
        public Climb Climb { get; set; }
        public Swim Swim { get; set; }
        public Persuade Persuade { get; set; }
        public Intimidate Intimidate { get; set; }



        public int HealthPerLevel { get; } = 5;
        public int MagicPerLevel { get; } = 5;
        public int CurrentHealth { get; set; }

        public int CurrentMagicPool { get; set; }
        public int MaximumHealth => Vitality.CurrentValue + (HealthPerLevel * CurrentLevel);
        public int MaximumMagicPool => Arcane.CurrentValue + Spirit.CurrentValue + (MagicPerLevel * CurrentLevel);

        public int MaximumEncumbrance => Strength.CurrentValue * 15;

        public int CurrentExperience { get; set; }

        public int NextLevelExperience => (CurrentLevel ^ 2) * 250 + CurrentLevel * 750;

        public int CurrentLevel { get; set; }


        public IDictionary<CharacterStatTypes, IStat> StatDictionary { get; }
        public IDictionary<DamageType, DamageResistance> Resistances { get; } = new Dictionary<DamageType, DamageResistance>();
        public IList<EquipmentSlotType> EquipmentSlotsTypes { get; }

        public CharacterStats(
            string name,
            string id,
            string portraitPath,
            string speciesId,
            Dictionary<CharacterStatTypes, int> stats,
            Dictionary<DamageType, DamageResistance> resistances = null,
            IList<EquipmentSlotType> equipmentSlots = null)
        {
            Id = id;
            Name = name;
            DisplayName = name;
            SpeciesId = speciesId;
            PortraitPath = portraitPath;

            //base stats
            Strength = new Strength(stats[CharacterStatTypes.Strength]);
            Vitality = new Vitality(stats[CharacterStatTypes.Vitality]);
            Agility = new Agility(stats[CharacterStatTypes.Agility]);
            Speed = new Speed(stats[CharacterStatTypes.Speed]);
            Charisma = new Charisma(stats[CharacterStatTypes.Charisma]);
            Intellect = new Intellect(stats[CharacterStatTypes.Intellect]);
            Spirit = new Spirit(stats[CharacterStatTypes.Spirit]);
            Arcane = new Arcane(stats[CharacterStatTypes.Arcane]);

            //derived stats
            Attack = new Attack(
                stats.ContainsKey(CharacterStatTypes.Attack) ? stats[CharacterStatTypes.Attack] : 0, 
                new CharacterStat[] { Strength, Speed });
            Defense = new Defense(
                stats.ContainsKey(CharacterStatTypes.Defense) ? stats[CharacterStatTypes.Defense] : 0, 
                new CharacterStat[] { Agility, Vitality });
            Initiative = new Initiative(
                stats.ContainsKey(CharacterStatTypes.Initiative) ? stats[CharacterStatTypes.Initiative] : 0, 
                new CharacterStat[] { Agility, Speed });
            ArcaneAffinity = new ArcaneAffinity(
                stats.ContainsKey(CharacterStatTypes.ArcaneAffinity) ? stats[CharacterStatTypes.ArcaneAffinity] : 0, 
                new CharacterStat[] { Intellect, Arcane });
            SpiritAffinity = new SpiritAffinity(
                stats.ContainsKey(CharacterStatTypes.SpiritAffinity) ? stats[CharacterStatTypes.SpiritAffinity] : 0, 
                new CharacterStat[] { Charisma, Spirit });

            //skills
            Climb = new Climb(
                stats.ContainsKey(CharacterStatTypes.Climb) ? stats[CharacterStatTypes.Climb] : 0);
            Swim = new Swim(stats.ContainsKey(CharacterStatTypes.Swim) ? stats[CharacterStatTypes.Swim] : 0);
            Persuade = new Persuade(stats.ContainsKey(CharacterStatTypes.Persuade) ? stats[CharacterStatTypes.Persuade] : 0);
            Intimidate = new Intimidate(stats.ContainsKey(CharacterStatTypes.Intimidate) ? stats[CharacterStatTypes.Intimidate] : 0);

            Resistances = resistances;
            EquipmentSlotsTypes = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();


            StatDictionary = new Dictionary<CharacterStatTypes, IStat>();
            var properties = typeof(CharacterStats).GetProperties();
            foreach (var property in properties)
            {

                if (typeof(IStat).IsAssignableFrom(property.PropertyType))
                {
                    var stat = (IStat)property.GetValue(this);
                    StatDictionary.Add(stat.Id, stat);
                }
            }

            CurrentHealth = MaximumHealth;
            CurrentMagicPool = MaximumMagicPool;
        }


    }
}
