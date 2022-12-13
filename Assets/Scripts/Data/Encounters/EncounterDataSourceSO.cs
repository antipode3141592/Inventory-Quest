using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public class EncounterDataSourceSO: SerializedMonoBehaviour, IEncounterDataSource
    {
        [OdinSerialize] List<IEncounterStats> encounters;

        public IEncounterStats GetById(string id)
        {
            var encounterStats = encounters.Find(x => x.Id == id);
            if (encounterStats is not null)
                return encounterStats;
            return null;
        }

        public IEncounterStats GetRandom()
        {
            int i = UnityEngine.Random.Range(0, encounters.Count);
            return encounters.ElementAt(i);
        }
    }
}
