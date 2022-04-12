using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public abstract class EncounterData
    {
        public string Id;

        public string Name;

        public string Description;

        public string Category;

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
            Category = stats.Category;
            Experience = stats.Experience;
            SuccessMessage = stats.SuccessMessage;
            FailureMessage = stats.FailureMessage;
            RewardIds = stats.RewardIds is null ? new List<string>() : stats.RewardIds.ToList();
            PenaltyIds = stats.PenaltyIds is null ? new List<string>() : stats.PenaltyIds.ToList();
        }
    }

    [Serializable]

    public class EncounterDataList
    {
        public List<SkillCheckEncounterData> SkillCheckEncounters = new List<SkillCheckEncounterData>();
        public List<RestEncounterData> RestEncounters = new List<RestEncounterData>();

        public EncounterDataList(List<IEncounterStats> encounters)
        {
            foreach (var encounter in encounters)
            {
                var skillEncounter = encounter as SkillCheckEncounterStats;
                if (skillEncounter is not null)
                    SkillCheckEncounters.Add(new SkillCheckEncounterData(skillEncounter));
                var restEncounter = encounter as RestEncounterStats;
                if (restEncounter is not null)
                    RestEncounters.Add(new RestEncounterData(restEncounter));
            }
        }
    }


    [Serializable]
    public class SkillCheckEncounterData: EncounterData
    {
        public List<SkillCheckRequirementData> SkillCheckRequirements = new();

        public SkillCheckEncounterData(SkillCheckEncounterStats stats) : base(stats)
        {
            foreach (var req in stats.SkillCheckRequirements)
            {
                SkillCheckRequirements.Add(new SkillCheckRequirementData(req));
            }
        }
    }

    [Serializable]
    public class SkillCheckRequirementData
    {
        public List<string> RequiredSkillNames = new ();
        public int TargetValue;
        public int PartyTargetValue;
        public SkillCheckRequirementData(SkillCheckRequirement requirement)
        {
            foreach (var skillType in requirement.SkillTypes)
            {
                RequiredSkillNames.Add(skillType.Name);
            }
            TargetValue = requirement.TargetValue;
            PartyTargetValue = requirement.PartyTargetValue;
        }

        public SkillCheckRequirementData(List<string> requiredSkillNames, int targetValue, int partyTargetValue)
        {
            RequiredSkillNames = requiredSkillNames;
            TargetValue = targetValue;
            PartyTargetValue = partyTargetValue;
        }
    }

    [Serializable]
    public class RestEncounterData : EncounterData
    {
        public RestEncounterData(RestEncounterStats stats) : base(stats)
        {
        }
    }
}
