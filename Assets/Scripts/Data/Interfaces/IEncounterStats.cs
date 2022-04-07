using System.Collections.Generic;


namespace Data.Interfaces
{
    public interface IEncounterStats
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string Category { get; } // display name for encounter Type

        public int Experience { get; }

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }
    }
}
