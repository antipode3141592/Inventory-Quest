using System.Collections.Generic;
using UnityEngine;

namespace Data.Locations
{
    public class LocationDataSourceScriptableObjectTest : ScriptableObject, ILocationDataSource
    {
        Dictionary<string, ILocationStats> locations = new()
        {
            {
                "Startington",
                new LocationStats(id: "Startington",
                name: "Startington",
                thumbnailSpritePath: "Locations/town_icon_1"
                )
            },
            {
                "Destinationville",
                new LocationStats(id: "Destinationville",
                name: "Destinationville",
                thumbnailSpritePath: "Locations/town_icon_2"
                )
            },
            {
                "forgotten_castle",
                new LocationStats(id: "forgotten_castle",
                name: "Forgotten Castle",
                thumbnailSpritePath: "Locations/forgotten_castle"
                )
            },
            {
                "forest_outpost",
                new LocationStats(id: "forest_outpost",
                name: "Forest Outpost",
                thumbnailSpritePath: "Locations/forest_outpost")
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