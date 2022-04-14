using System.Collections.Generic;

namespace Data.Encounters
{
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

        public RestEncounterStats(RestEncounterData encounterData)
        {
            Id = encounterData.Id;
            Name = encounterData.Name;
            Description = encounterData.Description;
            Experience = encounterData.Experience;
            SuccessMessage = encounterData.SuccessMessage;
            FailureMessage = encounterData.FailureMessage;
            RewardIds = encounterData.RewardIds;
            PenaltyIds = encounterData.PenaltyIds;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int Experience { get; }

        public string Category => "Rest";

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }

        
    }
}
