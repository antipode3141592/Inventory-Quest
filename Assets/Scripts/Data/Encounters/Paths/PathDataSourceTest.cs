using System.Collections.Generic;

namespace Data.Encounters
{
    public class PathDataSourceTest : IPathDataSource
    {
        public IPathStats GetPathById(string id)
        {
            return new PathStats(
                id: "intro_path", 
                name: "Deliver a letter", 
                startLocationId: "Startington", 
                endLocationId: "Destinationville", 
                encounterIds: new List<string> 
                { "intro_1",
                "intro_2",
                "intro_3"
                });
        }
    }
}