using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    public class SkillTest : IChoice
    {
        [SerializeField, TextArea(1, 5)] string description;
        [SerializeField] int targetValue;
        [SerializeField] int partyTargetValue;
        [EnumToggleButtons] public List<StatTypes> StatTypes = new();
        [EnumToggleButtons] public List<StatTypes> SkillTypes = new();
        [SerializeField] int experience;
        [SerializeField, TextArea(1, 5)] string successMessage;
        [SerializeField, TextArea(1, 5)] string failureMessage;
        [SerializeField] List<IRewardStats> rewards = new();
        [SerializeField] List<IPenaltyStats> penalties = new();

        public SkillTest(string description, int targetValue, int partyTargetValue, List<StatTypes> statTypes, List<StatTypes> skillTypes, int experience, string successMessage, string failureMessage, List<IRewardStats> rewards, List<IPenaltyStats> penalties)
        {
            this.description = description;
            this.targetValue = targetValue;
            this.partyTargetValue = partyTargetValue;
            StatTypes = statTypes;
            SkillTypes = skillTypes;
            this.experience = experience;
            this.successMessage = successMessage;
            this.failureMessage = failureMessage;
            this.rewards = rewards;
            this.penalties = penalties;
        }

        public string Description => description;
        public int Experience => experience;
        public int TargetValue => targetValue;
        public int PartyTargetValue => partyTargetValue;
        public string SuccessMessage => successMessage;
        public string FailureMessage => failureMessage;
        public List<IRewardStats> Rewards => rewards;
        public List<IPenaltyStats> Penalties => penalties;

        

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

        public bool Resolve(Party party)
        {
            int partyTotal = 0;
            int characterTotal = 0;
            foreach (var character in party.Characters.Values)
            {
                //int charSkillTotal = 0;
                int highestStat = 0;
                int highestSkill = 0;

                foreach (var stat in StatTypes)
                {
                    if (character.StatDictionary[stat].CurrentValue > highestStat) {
                        highestStat = character.StatDictionary[stat].CurrentValue;
                    }
                }
                foreach (var skill in SkillTypes)
                {
                    if (character.StatDictionary[skill].CurrentValue > highestSkill)
                    {
                        highestSkill = character.StatDictionary[skill].CurrentValue;
                    }
                }
                partyTotal += highestSkill + highestStat;
                characterTotal = highestSkill + highestStat > characterTotal ? highestSkill + highestStat : characterTotal;
            }
            if (characterTotal >= TargetValue)
                return true;
            return (PartyTargetValue > 0) && (partyTotal >= PartyTargetValue);
        }
    }
}


