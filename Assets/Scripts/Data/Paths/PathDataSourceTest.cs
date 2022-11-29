using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public class PathDataSourceTest : IPathDataSource
    {
        Dictionary<string, IPathStats> pathStats = new Dictionary<string, IPathStats>()
        {
            {
                "intro_path", new PathStats(
                id: "intro_path",
                name: "A Stroll to Destinationville",
                startLocationId: "Startington",
                endLocationId: "Destinationville",
                encounterStats: new List<IEncounterStats>
                { new EmptyEncounterStats( "intro_1")
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
            var stats = pathStats.Values.FirstOrDefault<IPathStats>(x => x.StartLocationId == startLocationId && x.EndLocationId == endLocationId);
            if (stats is null) return null;
            return stats;
        }
    }
}