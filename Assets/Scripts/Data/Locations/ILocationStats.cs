using System.Collections.Generic;
using UnityEngine;

namespace Data.Locations
{
    public interface ILocationStats
    {
        public string Id { get; }
        public string Name { get; }
        public string DisplayName { get; }

        public Sprite ThumbnailSprite { get; }

        public List<string> CharacterIds { get; }

        public List<string> LocationIds { get; }

        public bool IsKnown { get; }

        public string ScenePath { get; }
    }
}