using System.Collections.Generic;

namespace Data.Encounters
{
    public class PathStats : IPathStats
    {
        public PathStats(string id, string name, string startLocationId, string endLocationId, IList<string> encounterIds)
        {
            Id = id;
            Name = name;
            StartLocationId = startLocationId;
            EndLocationId = endLocationId;
            EncounterIds = encounterIds;
        }

        public string Id { get; }

        public string Name { get; }

        public string StartLocationId { get; }

        public string EndLocationId { get; }

        public IList<string> EncounterIds { get; }
    }
}