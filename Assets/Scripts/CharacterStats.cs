using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest
{

    [Serializable]
    public class CharacterStats
    {
        //primary stats


        public int Strength;
        public int Dexterity;
        public int Durability;

        public int FireResistance;
        public int IceResistance;
        public int AcidResistance;
        public int LightiningResistance;

        public CharacterStats(int strength, int dexterity, int durability, int fireResistance = 0, int iceResistance = 0, int acidResistance = 0, int lightiningResistance = 0)
        {
            Strength = strength;
            Dexterity = dexterity;
            Durability = durability;
            FireResistance = fireResistance;
            IceResistance = iceResistance;
            AcidResistance = acidResistance;
            LightiningResistance = lightiningResistance;
        }


        //derived stats
        public int MaxEncumbrance => Strength * 10;
        public int MaxHealth => Durability * 10;
    }

    [Serializable]
    public class CharacterStat
    {
        public string Name;
        public int CurrentValue;
        
    }

}
