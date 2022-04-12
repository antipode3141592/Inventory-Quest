using Data.Characters;
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
}


