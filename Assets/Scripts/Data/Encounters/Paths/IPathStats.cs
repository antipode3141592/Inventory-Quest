using System.Collections.Generic;

namespace Data.Encounters
{
    public interface IPathStats
    {
        public string Id { get; }

        public string Name { get; }

        public string StartLocationId { get; }

        public string EndLocationId { get; }

        public IList<string> EncounterIds { get; }
    }
}