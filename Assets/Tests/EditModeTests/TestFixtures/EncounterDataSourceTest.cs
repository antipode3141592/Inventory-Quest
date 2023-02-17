using Data;
using Data.Encounters;
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
                new TestEncounterStats(
                id: "test_of_might",
                name: "Test of Might",
                description: "A boulder blocks your path!  Move it, or go around (add a random encounter).",
                choices: new()
                    {
                        new SkillTest(description: "",
                            targetValue: 14,
                            partyTargetValue: 0,
                            statTypes: new() { StatTypes.Strength },
                            skillTypes: new(),
                            experience: 150,
                            successMessage: "",
                            failureMessage: "",
                            rewards: new(),
                            penalties: new())
                    }
                )
            },
            {
                "test_of_vigor",
                new TestEncounterStats(
                id: "test_of_vigor",
                name: "Test of Vigor",
                description: "A sheer cliff bars your way!  Climb it, or go around (add a random encounter). ",
                choices: new()
                {
                    new SkillTest(description: "",
                            targetValue: 16,
                            partyTargetValue: 0,
                            statTypes: new() { StatTypes.Vitality },
                            skillTypes: new() { StatTypes.Climb },
                            experience: 150,
                            successMessage: "",
                            failureMessage: "",
                            rewards: new(),
                            penalties: new())
                }
                )
            }
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

