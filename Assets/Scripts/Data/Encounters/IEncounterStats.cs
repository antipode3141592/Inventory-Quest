using System.Collections.Generic;


namespace Data.Encounters
{

    public interface IEncounterStats
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string Category { get; } // display name for encounter Type

        public int Experience { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }
    }
}
