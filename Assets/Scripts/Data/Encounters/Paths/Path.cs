using System.Collections.Generic;

namespace Data.Encounters
{
    /* Idle - in town, menu, or whatever
 * Pathfinding - pick endpoint location on map, load path (list of encounters) data
 * Encountering - iterate through list of encounters
 * 
 */

    public class Path : IPath
    {
        public Path(IPathStats stats)
        {
            Id = stats.Id;
            Name = stats.Name;
            StartLocationId = stats.StartLocationId;
            EndLocationId = stats.EndLocationId;
            EncounterIds = stats.EncounterIds;
            Stats = stats;
        }

        public string Id { get; }

        public string Name { get; }

        public string StartLocationId { get; }

        public string EndLocationId { get; }

        public IList<string> EncounterIds { get; }

        public IPathStats Stats { get; }
    }
}