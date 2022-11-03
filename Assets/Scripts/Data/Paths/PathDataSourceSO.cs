
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public class PathDataSourceSO: SerializedMonoBehaviour, IPathDataSource
    {
        [OdinSerialize] Dictionary<string, IPathStats> pathStats;

        public IPathStats GetPathById(string id)
        {
            if (!pathStats.ContainsKey(id)) return null;
            return pathStats[id];
        }

        public IPathStats GetPathForStartAndEndLocations(string startLocationId, string endLocationId)
        {
            var stats = pathStats.Values.FirstOrDefault<IPathStats>(x => x.StartLocationId == startLocationId && x.EndLocationId == endLocationId);
            if (stats is null) return null;
            return stats;
        }
    }
}