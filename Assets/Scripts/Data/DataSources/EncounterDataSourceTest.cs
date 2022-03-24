using Data.Interfaces;
using System.Collections.Generic;

namespace Data.Encounters
{
    public class EncounterDataSourceTest : IEncounterDataSource
    {
        Dictionary<string, IEncounterStats> encounterDictionary = new() 
        {
            { "test_of_might", new SkillCheckEncounterStats(
                id: "test_of_might",
                name: "Test of Might",
                description: "At least one Party Member must have the required Strength or face the consequences!",
                experience: 100,
                rewardIds: new List<string>() { "lootbox_common"},
                penaltyIds: new List<string>(){},
                skillCheckRequirements: new List<SkillCheckValue>() 
                {
                    { new PartySkillCheckValue(typeof(Strength),20,60) }
                }) 
            },
            {
                "test_of_vigor",
                new SkillCheckEncounterStats(
                id: "test_of_vigor",
                name: "Test of Vigor",
                description: "At least one Party Member must have the required Vitality or face the consequences!",
                experience: 100,
                rewardIds: new List<string>() { "lootbox_common" },
                penaltyIds: new List<string>() { },
                skillCheckRequirements: new List<SkillCheckValue>()
                {
                    { new SkillCheckValue(typeof(Vitality),20) }
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
            return null;
        }
    }
}
