using Data.Characters;
using System;
using System.Collections.Generic;

namespace Data.Encounters
{
    public class DialogueEncounter : Encounter
    {
        public DialogueEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            throw new NotImplementedException();
        }
    }

    public class DecisionEncounter : Encounter
    {
        public DecisionEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            throw new NotImplementedException();
        }
    }

    public class RestEncounter : Encounter
    {
        public RestEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            foreach(var character in party.Characters.Values)
            {

                //restore health and magic
                character.Stats.CurrentHealth = character.Stats.MaximumHealth;
            }
            return true;
        }
    }

    public class RestEncounterStats : IEncounterStats
    {
        public RestEncounterStats(string id, string name, string description, int experience, string successMessage, string failureMessage, IList<string> rewardIds, IList<string> penaltyIds = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Experience = experience;
            SuccessMessage = successMessage;
            FailureMessage = failureMessage;
            RewardIds = rewardIds;
            PenaltyIds = penaltyIds;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string Category => "Rest";

        public int Experience { get; }

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }
    }
}
