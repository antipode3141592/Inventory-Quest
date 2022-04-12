using Data.Characters;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Data.Encounters
{
    public class SkillCheckRequirement
    {
        public List<Type> SkillTypes = new();
        public int TargetValue;
        public int PartyTargetValue;

        public SkillCheckRequirement(List<Type> skillTypes, int targetValue, int partyTargetValue = -1)
        {
            SkillTypes = skillTypes;
            TargetValue = targetValue;
            PartyTargetValue = partyTargetValue;
        }

        public SkillCheckRequirement(SkillCheckRequirementData requirementData)
        {
            
            foreach(var skillName in requirementData.RequiredSkillNames)
            {
                var typeName = $"Data.Characters.{skillName}";
                SkillTypes.Add(Type.GetType(typeName: typeName));
            }
            TargetValue = requirementData.TargetValue;
            PartyTargetValue = requirementData.PartyTargetValue;

        }

        public override string ToString() 
        {
            string skillTypes = SkillTypes[0].Name;
            for (int i = 1; i < SkillTypes.Count; i++)
            {
                skillTypes += $" + {SkillTypes[i].Name}";
            }
            if (PartyTargetValue < 0)
                return $"{skillTypes} : {TargetValue}";
            return $"{skillTypes} : {TargetValue} or combined {PartyTargetValue}";
        }
    }
}


