using Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest
{

    [Serializable]
    public class CharacterStats
    {
        public CharacterStat Strength;
        public CharacterStat Dexterity;
        public CharacterStat Durability;
        public DamageResistance LightingResistance;
        public DamageResistance AcidResistance;
        public DamageResistance FireResistance;
        public DamageResistance IceResistance;

        public CharacterStats(float strength, float dexterity, float durability, float lightingResistance = 0f, float acidResistance = 0f, float fireResistance = 0f, float iceResistance = 0f)
        {
            Strength = new CharacterStat(strength);
            Dexterity = new CharacterStat(dexterity);
            Durability = new CharacterStat(durability);
            LightingResistance = new DamageResistance(DamageType.Lightning, lightingResistance);
            AcidResistance = new DamageResistance(DamageType.Acid, acidResistance);
            FireResistance = new DamageResistance(DamageType.Fire, fireResistance);
            IceResistance = new DamageResistance(DamageType.Ice, iceResistance);
        }
    }

    [Serializable]
    public class CharacterStat
    {
        public float InitialValue;

        public float CurrentValue { get; }

        public CharacterStat(float initialValue)
        {
            InitialValue = initialValue;
        }
    }

    [Serializable]
    public class DamageResistance
    {
        public DamageType DamageType;
        
        public float InitialValue;

        public DamageResistance(DamageType damageType, float initialValue)
        {
            DamageType = damageType;
            InitialValue = initialValue;
        }

        public float CurrentValue { get; }
    }
}
