using Data;
using Data.Encounters;
using Data.Penalties;
using Data.Rewards;
using System.Collections.Generic;
using System.Linq;

namespace InventoryQuest.Testing.Stubs
{

    public class EncounterDataSourceTest : IEncounterDataSource
    {
        Dictionary<string, IEncounterStats> encounterDictionary = new()
        {
            {
                "test_of_might",
                new SkillCheckEncounterStats(
                id: "test_of_might",
                name: "Test of Might",
                description: "A boulder blocks your path!  Move it, or go around (add a random encounter).",
                experience: 175,
                successMessage: "",
                failureMessage: "",
                rewards: new List<IRewardStats>() { },
                penalties: new List<IPenaltyStats>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                new SkillCheckRequirement(new () { StatTypes.Strength }, new(), 20, 60)
                })
            },
            {
                "test_of_vigor",
                new SkillCheckEncounterStats(
                id: "test_of_vigor",
                name: "Test of Vigor",
                description: "A sheer cliff bars your way!  Climb it, or go around (add a random encounter). ",
                experience: 150,
                successMessage: "",
                failureMessage: "",
                rewards: new List<IRewardStats>() { },
                penalties: new List<IPenaltyStats>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                { new SkillCheckRequirement(new () { StatTypes.Strength, StatTypes.Vitality }, new() {StatTypes.Climb}, 20) }
                })
            },
            {
                "test_of_faith",
                new SkillCheckEncounterStats(
                id: "test_of_faith",
                name: "Test of Faith",
                description: "An ominous fog fills the area.  The angered spirits can be appeased with a cleansing ritual.",
                experience: 300,
                successMessage: "",
                failureMessage: "",
                rewards: new List<IRewardStats>() { },
                penalties: new List<IPenaltyStats>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                    { new SkillCheckRequirement(new () { StatTypes.Spirit }, new(), 30) }
                })
            }
            //,
            //{
            //    "campsite",
            //    new RestEncounterStats(
            //        id: "campsite",
            //        name: "An Ideal Campsite",
            //        description: "A lovely place to rest for the evening.  (Restore all HP and MP)",
            //        experience: 0,
            //        successMessage: "",
            //        failureMessage: "",
            //        rewardIds: null
            //    )
            //}

        };

        public EncounterDataSourceTest()
        {

        }


        public IEncounterStats GetById(string id)
        {
            if (!encounterDictionary.ContainsKey(id)) return null;
            return encounterDictionary[id];
        }

        public IEncounterStats GetRandom()
        {
            int i = UnityEngine.Random.Range(0, encounterDictionary.Count);
            return encounterDictionary.ElementAt(i).Value;
        }
    }
}

