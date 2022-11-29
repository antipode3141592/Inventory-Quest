﻿using Data.Characters;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Encounters
{
    public class SkillCheckRequirement
    {
        [SerializeField, TextArea(1,5)] public string Description;
        [EnumToggleButtons] public List<StatTypes> StatTypes = new();
        [EnumToggleButtons] public List<StatTypes> SkillTypes = new();
        public int TargetValue;
        public int PartyTargetValue;

        public SkillCheckRequirement(List<StatTypes> statTypes, List<StatTypes> skillTypes, int targetValue, int partyTargetValue = -1)
        {
            StatTypes = statTypes;
            SkillTypes = skillTypes;
            TargetValue = targetValue;
            PartyTargetValue = partyTargetValue;
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


