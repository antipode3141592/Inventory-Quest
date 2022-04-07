using Data.Interfaces;
using System;
using System.Collections.Generic;


namespace Data.Encounters
{
    public abstract class Encounter : IEncounter
    {
        protected Encounter(IEncounterStats encounterStats)
        {
            GuId = Guid.NewGuid().ToString();
            RewardIds = encounterStats.RewardIds;
            PenaltyIds = encounterStats.PenaltyIds;
            Stats = encounterStats;
        }

        public string GuId { get; }

        public string Id => Stats.Id;

        public string Name => Stats.Name;

        public string Description => Stats.Description;

        public int Experience => Stats.Experience;

        public IEncounterStats Stats { get; }

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }

        public abstract bool Resolve(Party party);
    }
}
