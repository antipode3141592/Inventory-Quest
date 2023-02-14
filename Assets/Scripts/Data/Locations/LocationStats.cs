using System.Collections.Generic;
using UnityEngine;

namespace Data.Locations
{
    public class LocationStats : ILocationStats
    {
        public LocationStats(string id, string name, List<string> characterIds = null, List<string> locationIds = null, Sprite thumbnailSprite = null, bool isKnown = false, bool isTravelPoint = false, string scenePath = "", AudioClip audioClip = null)
        {
            Id = id;
            Name = name;
            CharacterIds = characterIds;
            LocationIds = locationIds;
            DisplayName = name;
            ThumbnailSprite = thumbnailSprite;
            IsKnown = isKnown;
            IsTravelPoint = isTravelPoint;
            ScenePath = scenePath;
            LocationMusic = audioClip;
         }

        public string Id { get; }

        public string Name { get; }

        public string DisplayName { get; protected set; }

        public List<string> CharacterIds { get; }

        public List<string> LocationIds { get; }

        public Sprite ThumbnailSprite { get; }

        public bool IsKnown { get; }

        public string ScenePath { get; }

        public AudioClip LocationMusic { get; }

        public bool IsTravelPoint { get; }
    }
}
