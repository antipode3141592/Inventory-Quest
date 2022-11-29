using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using System.Collections.Generic;

namespace Data.Encounters
{
    public interface IEncounter
    {
        public string GuId { get; }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }

        public int Experience { get; }

        public IEncounterStats Stats { get; }

        public IList<IRewardStats> Rewards { get; }

        public IList<IPenaltyStats> Penalties { get; }
        public bool Resolve(Party party);


    }
}