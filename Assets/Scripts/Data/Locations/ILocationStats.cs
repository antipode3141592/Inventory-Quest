using System.Collections.Generic;

namespace Data.Locations
{
    public interface ILocationStats
    {
        public string Id { get; }
        public string Name { get; }
        public string DisplayName { get; }

        public string ThumbnailSpritePath { get; }

        public IList<string> CharacterIds { get; }

        public IList<string> LocationIds { get; }

        public bool IsKnown { get; }
    }
}