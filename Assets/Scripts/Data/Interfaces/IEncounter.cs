using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IEncounter
    {
        public string GuId { get; }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }

        public int Experience { get; }

        public IEncounterStats Stats { get; }

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }

        public bool Resolve(Party party);


    }
}