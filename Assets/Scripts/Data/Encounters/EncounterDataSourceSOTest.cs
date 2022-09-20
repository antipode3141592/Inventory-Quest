using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public class EncounterDataSourceSOTest: SerializedMonoBehaviour, IEncounterDataSource
    {
        [OdinSerialize] Dictionary<string, IEncounterStats> encounterDictionary;

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
