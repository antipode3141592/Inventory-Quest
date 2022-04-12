using System.Collections.Generic;


namespace Data.Encounters
{
    public interface IPath
    {
        public string Id { get; }
        public string Name { get; }

        public string StartLocationId { get; }
        public string EndLocationId { get; }

        public int Length => EncounterIds.Count;

        public IList<string> EncounterIds { get; }
    }
}
