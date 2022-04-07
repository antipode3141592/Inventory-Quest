using System;
using System.Collections.Generic;

namespace Data.Encounters
{
    public class SkillCheckEncounter : Encounter
    {


        public IList<SkillCheckRequirement> SkillCheckRequirements;

        public SkillCheckEncounter(SkillCheckEncounterStats stats) : base(stats)
        {
            SkillCheckRequirements = stats.SkillCheckRequirements;
        }

        public override bool Resolve(Party party)
        {
            if (CheckList(party, SkillCheckRequirements)) return true;
            return false;
        }

        bool CheckList(Party party, IList<SkillCheckRequirement> skillChecks)
        {
            foreach (var skillCheck in skillChecks)
            {
                if (Check(party, skillCheck)) { return true; }
            }
            return false;
        }

        public bool Check(Party party, SkillCheckRequirement skillCheck)
        {
            int currentTotal = 0;
            int maxValue = 0;
            foreach (var character in party.Characters.Values)
            {
                int charSkillTotal = 0;
                foreach (var type in skillCheck.SkillTypes)
                {
                    charSkillTotal += character.Stats.Stats[type].CurrentValue;
                }
                currentTotal += charSkillTotal;
                maxValue = charSkillTotal > maxValue ? charSkillTotal : maxValue;
            }
            if (maxValue >= skillCheck.TargetValue) 
                return true;
            return (skillCheck.PartyTargetValue >= 0) && (currentTotal >= skillCheck.PartyTargetValue);
        }
    }

    public class SkillCheckRequirement
    {
        public IList<Type> SkillTypes;
        public int TargetValue;
        public int PartyTargetValue;

        public SkillCheckRequirement(IList<Type> skillTypes, int targetValue, int partyTargetValue = -1)
        {
            SkillTypes = skillTypes;
            TargetValue = targetValue;
            PartyTargetValue = partyTargetValue;
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


