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
            Id = encounterStats.Id;
            Name = encounterStats.Name;
            Description = encounterStats.Description;
            RewardIds = encounterStats.RewardIds;
            PenaltyIds = encounterStats.PenaltyIds;
            Experience = encounterStats.Experience;
        }

        public string GuId { get; }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int Experience { get; }

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }

        public abstract bool Resolve(Party party);
    }
}
