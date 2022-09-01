using Data.Characters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

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
                successMessage: "",
                failureMessage: "",
                rewardIds: new List<string>() { "common_loot_pile_medium" },
                penaltyIds: new List<string>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                    new SkillCheckRequirement(new List<Type>() { typeof(Strength) }, 20, 60)
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
                rewardIds: new List<string>() { "common_loot_pile_medium" },
                penaltyIds: new List<string>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                    { new SkillCheckRequirement(new List<Type>() { typeof(Vitality) }, 20) },
                    { new SkillCheckRequirement(new List<Type>() { typeof(Strength) }, 20) }
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
                rewardIds: new List<string>() { "common_loot_pile_medium", "common_loot_pile_medium" },
                penaltyIds: new List<string>() { },
                skillCheckRequirements: new List<SkillCheckRequirement>()
                {
                    { new SkillCheckRequirement(new List<Type>() { typeof(Spirit) }, 30) }
                })
            },
            {
                "campsite",
                new RestEncounterStats(
                    id: "campsite",
                    name: "An Ideal Campsite",
                    description: "A lovely place to rest for the evening.  (Restore all HP and MP)",
                    experience: 0,
                    successMessage: "",
                    failureMessage: "",
                    rewardIds: null
                )
            }

        };

        readonly string _filename = "encounterData.json"; //default file name

        public EncounterDataSourceTest()
        {

        }

        public void SaveEncounterData()
        {
            string saveFilename = PrependPersistencePath(_filename);

            EncounterDataList encounterData = new(encounterDictionary.Values.ToList());

            string json = JsonUtility.ToJson(obj: encounterData, prettyPrint: true);

            Debug.Log(json);

            FileStream filestream = new FileStream(saveFilename, FileMode.Create);
            //using syntax automatically opens and closes the filestream cleanly
            using (StreamWriter writer = new StreamWriter(filestream))
            {
                writer.Write(json);
            }


        }


        public IEncounterStats GetById(string id)
        {
            if(!encounterDictionary.ContainsKey(id)) return null;
            return encounterDictionary[id];
        }

        public IEncounterStats GetRandom()
        {
            int i = UnityEngine.Random.Range(0, encounterDictionary.Count);
            return encounterDictionary.ElementAt(i).Value;
        }

        string PrependPersistencePath(string filename)
        {
            return Application.persistentDataPath + "/" + filename; //slash, because it's from inside unity
        }
    }
}
