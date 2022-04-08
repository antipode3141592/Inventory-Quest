using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public class EncounterDataSourceTest : IEncounterDataSource
    {
        Dictionary<string, IEncounterStats> encounterDictionary = new() 
        {
            { "test_of_might", new SkillCheckEncounterStats(
                id: "test_of_might",
                name: "Test of Might",
                description: "A boulder blocks your path!  Move it, or go around (add a random encounter).",
                experience: 175,
                rewardIds: new List<string>() { "common_loot_pile_medium" },
                penaltyIds: new List<string>(){},
                skillCheckRequirements: new List<SkillCheckRequirement>() 
                {
                    new SkillCheckRequirement(new List<Type>(){typeof(Strength) },20,60)
                }) 
            },
            {
                "test_of_vigor",
                new SkillCheckEncounterStats(
                id: "test_of_vigor",
                name: "Test of Vigor",
                description: "A sheer cliff bars your way!  Climb it, or go around (add a random encounter). ",
                experience: 150,
                rewardIds: new List<string>() { "common_loot_pile_medium" },
                penaltyIds: new List<string>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                    { new SkillCheckRequirement(new List<Type>(){typeof(Vitality)},20) },
                    { new SkillCheckRequirement(new List<Type>(){typeof(Strength)},20) }
                })
            },
            {
                "test_of_faith",
                new SkillCheckEncounterStats(
                id: "test_of_faith",
                name: "Test of Faith",
                description: "An ominous fog fills the area.  The angered spirits can be appeased with a cleansing ritual.",
                experience: 300,
                rewardIds: new List<string>() { "common_loot_pile_medium", "common_loot_pile_medium" },
                penaltyIds: new List<string>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                    { new SkillCheckRequirement(new List<Type>(){typeof(Spirit) },30) }
                })
            }

        };

        public EncounterDataSourceTest()
        {

        }

        public IEncounterStats GetEncounterById(string id)
        {
            if(!encounterDictionary.ContainsKey(id)) return null;
            return encounterDictionary[id];
        }

        public IEncounterStats GetRandomEncounter()
        {
            int i = UnityEngine.Random.Range(0, encounterDictionary.Count);
            return encounterDictionary.ElementAt(i).Value;
        }
    }

    //public class EncounterDataSourceJSON : IEncounterDataSource
    //{


    //    public IEncounterStats GetEncounterById(string id)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public IEncounterStats GetRandomEncounter()
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
