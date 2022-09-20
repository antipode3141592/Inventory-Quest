using System;
using System.Collections.Generic;
using Data.Characters;

namespace Data.Encounters
{
    [Serializable]
    public class SkillCheckRequirementData
    {
        public List<CharacterStatTypes> RequiredStatsTypes = new ();
        public List<CharacterStatTypes> RequiredSkillTypes = new ();
        public int TargetValue;
        public int PartyTargetValue;
        public SkillCheckRequirementData(SkillCheckRequirement requirement)
        {
            foreach (var skillType in requirement.SkillTypes)
                RequiredSkillTypes.Add(skillType);
            foreach (var statType in requirement.StatTypes)
                RequiredStatsTypes.Add(statType);
            TargetValue = requirement.TargetValue;
            PartyTargetValue = requirement.PartyTargetValue;
        }

        public SkillCheckRequirementData(List<CharacterStatTypes> requiredStatsTypes, List<CharacterStatTypes> requiredSkillTypes, int targetValue, int partyTargetValue)
        {
            RequiredStatsTypes = requiredStatsTypes;    
            RequiredSkillTypes = requiredSkillTypes;
            TargetValue = targetValue;
            PartyTargetValue = partyTargetValue;
        }
    }
}
