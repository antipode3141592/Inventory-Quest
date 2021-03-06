using System;
using System.Collections.Generic;


namespace Data.Encounters
{
    public class SkillCheckEncounterStats : IEncounterStats
    {
        public SkillCheckEncounterStats(string id, string name, string description, int experience, string successMessage, string failureMessage, IList<string> rewardIds, IList<string> penaltyIds, IList<SkillCheckRequirement> skillCheckRequirements)
        {
            Id = id;
            Name = name;
            Description = description;
            Experience = experience;
            SuccessMessage = successMessage;
            FailureMessage = failureMessage;
            RewardIds = rewardIds;
            PenaltyIds = penaltyIds;
            SkillCheckRequirements = skillCheckRequirements;
        }

        public SkillCheckEncounterStats(SkillCheckEncounterData encounterData)
        {
            Id = encounterData.Id;
            Name = encounterData.Name;
            Description = encounterData.Description;
            Experience = encounterData.Experience;
            SuccessMessage = encounterData.SuccessMessage;
            FailureMessage = encounterData.FailureMessage;
            RewardIds = encounterData.RewardIds;
            PenaltyIds = encounterData.PenaltyIds;

            SkillCheckRequirements = new List<SkillCheckRequirement>();
            foreach(var requirements in encounterData.SkillCheckRequirements)
            {
                SkillCheckRequirements.Add(new SkillCheckRequirement(requirements));
            }
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int Experience { get; }

        public string Category => "Skill Check";

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }

        public IList<SkillCheckRequirement> SkillCheckRequirements;
    }

    
}
