using System.Collections.Generic;

namespace Data.Locations
{
    public class LocationStats : ILocationStats
    {
        public LocationStats(string id, string name, IList<string> characterIds = null, IList<string> locationIds = null, string thumbnailSpritePath = null)
        {
            Id = id;
            Name = name;
            CharacterIds = characterIds;
            LocationIds = locationIds;
            DisplayName = name;
            ThumbnailSpritePath = thumbnailSpritePath;
        }

        public string Id { get; }

        public string Name { get; }

        public string DisplayName { get; protected set; }

        public IList<string> CharacterIds { get; }

        public IList<string> LocationIds { get; }

        public string ThumbnailSpritePath { get; }
    }
}
