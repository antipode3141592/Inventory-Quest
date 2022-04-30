using System;
using System.Collections.Generic;

namespace Data.Encounters
{
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
}
