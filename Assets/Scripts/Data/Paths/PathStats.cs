using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    public class PathStats : IPathStats
    {
        public PathStats(string id, string name, string startLocationId, string endLocationId, List<IEncounterStats> encounterStats)
        {
            Id = id;
            Name = name;
            StartLocationId = startLocationId;
            EndLocationId = endLocationId;
            EncounterStats = encounterStats;
        }

        public string Id { get; }

        public string Name { get; }

        public string StartLocationId { get; }

        public string EndLocationId { get; }

        public List<IEncounterStats> EncounterStats { get; }

        public AudioClip AudioClip { get; }
    }
}