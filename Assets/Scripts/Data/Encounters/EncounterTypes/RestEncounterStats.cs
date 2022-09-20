using System.Collections.Generic;

namespace Data.Encounters
{
    public class RestEncounterStats : IRestEncounterStats
    {
        public RestEncounterStats(string id, string name, string description, int experience, string successMessage, string failureMessage, List<string> rewardIds, List<string> penaltyIds = null)
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

        public List<string> RewardIds { get; }

        public List<string> PenaltyIds { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }

        
    }
}
