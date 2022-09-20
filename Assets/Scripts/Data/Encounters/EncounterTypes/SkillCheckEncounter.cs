using Data.Characters;
using System.Collections.Generic;
using System.Linq;

namespace Data.Encounters
{
    public class SkillCheckEncounter : Encounter
    {
        public IList<SkillCheckRequirement> SkillCheckRequirements;

        public SkillCheckEncounter(ISkillCheckEncounterStats stats) : base(stats)
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
            int partyTotal = 0;
            int characterTotal = 0;
            foreach (var character in party.Characters.Values)
            {
                //int charSkillTotal = 0;
                int highestStat = 0;
                int highestSkill = 0;

                foreach (var stat in skillCheck.StatTypes)
                {
                    highestStat = character.Stats.StatDictionary[stat].CurrentValue > highestStat ? character.Stats.StatDictionary[stat].CurrentValue : highestStat;
                    //charSkillTotal += character.Stats.StatDictionary[stat].CurrentValue;
                }
                foreach (var skill in skillCheck.SkillTypes)
                {
                    highestSkill = character.Stats.StatDictionary[skill].CurrentValue > highestSkill ? character.Stats.StatDictionary[skill].CurrentValue : highestSkill;
                }
                partyTotal += highestSkill + highestStat;
                characterTotal = highestSkill + highestStat > characterTotal ? highestSkill + highestStat : characterTotal;
            }
            if (characterTotal >= skillCheck.TargetValue) 
                return true;
            return (skillCheck.PartyTargetValue > 0) && (partyTotal >= skillCheck.PartyTargetValue);
        }
    }
}


