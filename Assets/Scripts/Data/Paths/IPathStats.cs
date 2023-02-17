using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    public interface IPathStats
    {
        public string Id { get; }
        public string Name { get; }
        public string StartLocationId { get; }
        public string EndLocationId { get; }
        public List<IEncounterStats> EncounterStats { get; }
        public AudioClip AudioClip { get; }
    }
}