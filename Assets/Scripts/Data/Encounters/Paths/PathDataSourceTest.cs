
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
                encounterIds: new List<string>
                { "intro_1",
                "intro_2",
                "intro_3"
                })
            },
            {
                "intro_return_path", new PathStats(
                id: "intro_return_path",
                name: "The Return to Startington",
                startLocationId: "Destinationville",
                endLocationId: "Startington",
                encounterIds: new List<string>
                {
                    "intro_return_1",
                    "intro_return_2",
                    "intro_return_3"
                }) 
            },
            {
                "path_startington_forgotten_castle", new PathStats(
                id: "path_startington_forgotten_castle",
                name: "The Return to Startington",
                startLocationId: "Startington",
                endLocationId: "forgotten_castle",
                encounterIds: new List<string>
                {
                    "castle_path_1",
                    "castle_path_2",
                })
            },
            {
                "path_forgotten_castle_startington", new PathStats(
                id: "path_forgotten_castle_startington",
                name: "The Return to Startington",
                startLocationId: "forgotten_castle",
                endLocationId: "Startington",
                encounterIds: new List<string>
                {
                    "castle_return_1",
                    "castle_return_2",
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