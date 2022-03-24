using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;

namespace Data.Encounters
{
    public class SkillCheckEncounter : Encounter
    {
        public IList<SkillCheckValue> SkillCheckRequirements;
        public IList<SkillCheckValue> SkillCheckAlternates;

        public SkillCheckEncounter(SkillCheckEncounterStats stats) : base(stats)
        {
            SkillCheckRequirements = stats.SkillCheckRequirements;
            SkillCheckAlternates = stats.SkillCheckAlternates;
        }

        public override bool Resolve(Party party)
        {
            //requirements
            if (CheckList(party, SkillCheckRequirements)) return true;
            //alternates
            if (CheckList(party, SkillCheckAlternates)) return true;
            return false;
                

        }

        bool CheckList(Party party, IList<SkillCheckValue> skillChecks)
        {
            if (skillChecks is null) return false;
            using (var pooledObject = ListPool<bool>.Get(out List<bool> resultsList))
            {
                foreach (var skillCheck in skillChecks)
                {
                    var partyCheck = skillCheck as PartySkillCheckValue;
                    if (partyCheck is not null)
                        resultsList.Add(PartyCheck(party, partyCheck) || CharacterCheck(party, partyCheck));
                    else
                        resultsList.Add(CharacterCheck(party, skillCheck));
                }
                if (!resultsList.Any(x => x == false))
                    return true;
                return false;
            }
        }

        bool CharacterCheck(Party party, SkillCheckValue skillCheckValue) 
        {
            return party.Characters.Values.Any(x => x.Stats.Stats[skillCheckValue.SkillType].CurrentValue >= skillCheckValue.SingleTargetValue);
        }

        bool PartyCheck(Party party, PartySkillCheckValue partyCheckValue)
        {
            int partySum = party.Characters.Values.Sum(x => x.Stats.Stats[partyCheckValue.SkillType].CurrentValue);
            if (partySum >= partyCheckValue.PartyTargetValue)
                return true;
            return false;
        }
    }

    public class SkillCheckValue
    {
        public Type SkillType;
        public int SingleTargetValue;

        public SkillCheckValue(Type skillType, int singleTargetValue)
        {
            SkillType = skillType;
            SingleTargetValue = singleTargetValue;
        }
    }

    public class PartySkillCheckValue : SkillCheckValue
    {
        public int PartyTargetValue;

        public PartySkillCheckValue(Type skillType, int singleTargetValue, int partyTargetValue) : base(skillType, singleTargetValue)
        {
            PartyTargetValue = partyTargetValue;
        }
    }
}


