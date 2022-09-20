using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public abstract class EncounterData
    {
        public string Id;

        public string Name;

        public string Description;


        public int Experience;

        public string SuccessMessage;

        public string FailureMessage;

        public List<string> RewardIds;

        public List<string> PenaltyIds;

        public EncounterData(IEncounterStats stats)
        {
            Id = stats.Id;
            Name = stats.Name;
            Description = stats.Description;
            Experience = stats.Experience;
            SuccessMessage = stats.SuccessMessage;
            FailureMessage = stats.FailureMessage;
            RewardIds = stats.RewardIds is null ? new List<string>() : stats.RewardIds.ToList();
            PenaltyIds = stats.PenaltyIds is null ? new List<string>() : stats.PenaltyIds.ToList();
        }
    }
}
