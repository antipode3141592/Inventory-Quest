using Data.Characters;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Data.Encounters
{
    [Serializable]
    public class SkillCheckRequirement
    {
        [EnumToggleButtons] public List<CharacterStatTypes> StatTypes = new();
        [EnumToggleButtons] public List<CharacterStatTypes> SkillTypes = new();
        public int TargetValue;
        public int PartyTargetValue;

        public SkillCheckRequirement(List<CharacterStatTypes> statTypes, List<CharacterStatTypes> skillTypes, int targetValue, int partyTargetValue = -1)
        {
            StatTypes = statTypes;
            SkillTypes = skillTypes;
            TargetValue = targetValue;
            PartyTargetValue = partyTargetValue;
        }

        public SkillCheckRequirement(SkillCheckRequirementData requirementData)
        {
            
            foreach (var skill in requirementData.RequiredSkillTypes)
                SkillTypes.Add(skill);
            foreach (var stat in requirementData.RequiredStatsTypes)
                StatTypes.Add(stat);
            TargetValue = requirementData.TargetValue;
            PartyTargetValue = requirementData.PartyTargetValue;

        }

        public override string ToString() 
        {
            string statTypes = $"{StatTypes[0]}";
            for (int i = 1; i < StatTypes.Count; i++)
                statTypes += $" or {StatTypes[i]}";
            if (SkillTypes.Count > 0)
            {
                string skillTypes = $"{SkillTypes[0]}";
                for (int j = 1; j < SkillTypes.Count; j++)
                    skillTypes += $" or {SkillTypes[j]}";
                if (PartyTargetValue <= 0)
                    return $"{statTypes} + {skillTypes} : {TargetValue}";
                return $"{statTypes} + {skillTypes} : {TargetValue} or party {PartyTargetValue}";
            }
            else
            {
                if (PartyTargetValue <= 0)
                    return $"{statTypes} : {TargetValue}";
                return $"{statTypes} : {TargetValue} or party {PartyTargetValue}";
            }
        }
    }
}


