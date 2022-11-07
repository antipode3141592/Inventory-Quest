using Data.Encounters;
using Data.Penalties;
using Data.Rewards;
using System.Collections.Generic;

namespace InventoryQuest.Testing.Stubs
{
    public class SkillCheckEncounterStats : ISkillCheckEncounterStats
    {
        public SkillCheckEncounterStats(string id, string name, string description, int experience, string successMessage, string failureMessage, List<IRewardStats> rewards, List<IPenaltyStats> penalties, List<SkillCheckRequirement> skillCheckRequirements)
        {
            Id = id;
            Name = name;
            Description = description;
            Experience = experience;
            SuccessMessage = successMessage;
            FailureMessage = failureMessage;
            Rewards = rewards;
            Penalties = penalties;
            SkillCheckRequirements = skillCheckRequirements;
        }
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int Experience { get; }

        public string Category => "Skill Check";

        public string SuccessMessage { get; }

        public string FailureMessage { get; }

        public List<SkillCheckRequirement> SkillCheckRequirements { get; }

        public List<IRewardStats> Rewards { get; }

        public List<IPenaltyStats> Penalties { get; }
    }
}
