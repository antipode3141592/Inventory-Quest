using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data.Encounters
{
    public class EncounterDataSourceJSON : IEncounterDataSource
    {
        readonly string _filename;

        Dictionary<string, IEncounterStats> encounterDictionary = new();

        public EncounterDataSourceJSON(string filename = "encounterData.json")
        {
            _filename = filename;
            LoadEncounterData();
        }

        public IEncounterStats GetEncounterById(string id)
        {
            if (!encounterDictionary.ContainsKey(id)) return null;
            return encounterDictionary[id];
        }

        public IEncounterStats GetRandomEncounter()
        {
            int i = UnityEngine.Random.Range(0, encounterDictionary.Count);
            return encounterDictionary.ElementAt(i).Value;
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

        void LoadEncounterData()
        {
            string loadFileName = PrependPersistencePath(_filename);
            if (File.Exists(loadFileName))
            {
                using (StreamReader reader = new StreamReader(loadFileName))
                {
                    string json = reader.ReadToEnd();
                    EncounterDataList encounterList = JsonUtility.FromJson<EncounterDataList>(json: json);

                    foreach (var skillEncounter in encounterList.SkillCheckEncounters)
                    {
                        encounterDictionary.Add(key: skillEncounter.Id,
                            new SkillCheckEncounterStats(skillEncounter));
                    }
                    foreach (var restEncounter in encounterList.RestEncounters) 
                    { 
                        encounterDictionary.Add(key: restEncounter.Id, 
                            new RestEncounterStats(restEncounter));
                    }
                }
            }
        }

        string PrependPersistencePath(string filename)
        {
            return Application.persistentDataPath + "/" + filename; //slash, because it's from inside unity
        }
    }
}
