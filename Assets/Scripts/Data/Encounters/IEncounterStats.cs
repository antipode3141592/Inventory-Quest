using System.Collections.Generic;


namespace Data.Encounters
{

    public interface IEncounterStats
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int Experience { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }

        public List<string> RewardIds { get; }

        public List<string> PenaltyIds { get; }
    }
}
