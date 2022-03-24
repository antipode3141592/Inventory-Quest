using Data.Interfaces;
using System.Collections.Generic;


namespace Data.Encounters
{
    public class SkillCheckEncounterStats : IEncounterStats
    {
        public SkillCheckEncounterStats(string id, string name, string description, int experience, IList<string> rewardIds, IList<string> penaltyIds, IList<SkillCheckValue> skillCheckRequirements, IList<SkillCheckValue> skillCheckAlternates = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Experience = experience;
            RewardIds = rewardIds;
            PenaltyIds = penaltyIds;
            SkillCheckRequirements = skillCheckRequirements;
            SkillCheckAlternates = skillCheckAlternates;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public int Experience { get; }

        public IList<string> RewardIds { get; }

        public IList<string> PenaltyIds { get; }

        public IList<SkillCheckValue> SkillCheckRequirements;
        public IList<SkillCheckValue> SkillCheckAlternates;
    }

    
}
