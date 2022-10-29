using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using System;
using System.Collections.Generic;


namespace Data.Encounters
{
    public abstract class Encounter : IEncounter
    {
        protected Encounter(IEncounterStats encounterStats)
        {
            GuId = Guid.NewGuid().ToString();
            Rewards = encounterStats.Rewards;
            Penalties = encounterStats.Penalties;
            Stats = encounterStats;
        }

        public string GuId { get; }

        public string Id => Stats.Id;

        public string Name => Stats.Name;

        public string Description => Stats.Description;

        public int Experience => Stats.Experience;

        public IEncounterStats Stats { get; }

        public IList<IRewardStats> Rewards { get; }

        public IList<IPenaltyStats> Penalties { get; }

        public abstract bool Resolve(Party party);
    }
}
