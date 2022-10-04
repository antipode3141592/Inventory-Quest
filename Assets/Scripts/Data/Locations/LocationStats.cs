using System.Collections.Generic;

namespace Data.Locations
{
    public class LocationStats : ILocationStats
    {
        public LocationStats(string id, string name, List<string> characterIds = null, List<string> locationIds = null, string thumbnailSpritePath = null, bool isKnown = false, string scenePath = "")
        {
            Id = id;
            Name = name;
            CharacterIds = characterIds;
            LocationIds = locationIds;
            DisplayName = name;
            ThumbnailSpritePath = thumbnailSpritePath;
            IsKnown = isKnown;
            ScenePath = scenePath;
         }

        public string Id { get; }

        public string Name { get; }

        public string DisplayName { get; protected set; }

        public List<string> CharacterIds { get; }

        public List<string> LocationIds { get; }

        public string ThumbnailSpritePath { get; }

        public bool IsKnown { get; }

        public string ScenePath { get; }
    }
}
