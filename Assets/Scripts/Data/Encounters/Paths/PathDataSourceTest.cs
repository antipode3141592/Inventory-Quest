
using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public class PathDataSourceTest : IPathDataSource
    {
        Dictionary<string, IPathStats> pathStats = new Dictionary<string, IPathStats>()
        {
            {"intro_path", new PathStats(
                id: "intro_path",
                name: "Deliver a letter",
                startLocationId: "Startington",
                endLocationId: "Destinationville",
                encounterIds: new List<string>
                { "intro_1",
                "intro_2",
                "intro_3"
                })
            }
        };

        public IPathStats GetPathById(string id)
        {
            if (!pathStats.ContainsKey(id)) return null;
            return pathStats[id];
        }

        public IPathStats GetPathForStartAndEndLocations(string startLocationId, string endLocationId)
        {
            var stats = pathStats.Values.First<IPathStats>(x => x.StartLocationId == startLocationId && x.EndLocationId == endLocationId);
            if (stats is null) return null;
            return stats;
        }
    }
}