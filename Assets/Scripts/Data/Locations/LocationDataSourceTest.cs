using System.Collections.Generic;

namespace Data.Locations
{
    public class LocationDataSourceTest : ILocationDataSource
    {
        Dictionary<string, ILocationStats> locations = new()
        {
            {
                "Startington", new LocationStats(id: "Startington",
                name: "Startington",
                thumbnailSpritePath: "Locations/town_icon_1",
                isKnown: true
                ) },
            { 
                "Destinationville", new LocationStats(id: "Destinationville", 
                name: "Destinationville",
                thumbnailSpritePath: "Locations/town_icon_2",
                isKnown: true
                ) },
            {
                "forgotten_castle", new LocationStats(id: "forgotten_castle",
                name: "Forgotten Castle",
                thumbnailSpritePath: "Locations/fortress_icon_1"
                ) },
            {
                "forest_outpost", new LocationStats(id: "forest_outpost",
                name: "Forest Outpost",
                thumbnailSpritePath: "Locations/temple_icon_1")
            }
        };

        public ILocationStats GetById(string id)
        {
            if (!locations.ContainsKey(id)) return null;
            return locations[id];
        }

        public ILocationStats GetRandom()
        {
            throw new System.NotImplementedException();
        }
    }
}