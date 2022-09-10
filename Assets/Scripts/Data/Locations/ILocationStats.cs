using System.Collections.Generic;

namespace Data.Locations
{
    public interface ILocationStats
    {
        public string Id { get; }
        public string Name { get; }
        public string DisplayName { get; }

        public string ThumbnailSpritePath { get; }

        public List<string> CharacterIds { get; }

        public List<string> LocationIds { get; }

        public bool IsKnown { get; }
    }
}