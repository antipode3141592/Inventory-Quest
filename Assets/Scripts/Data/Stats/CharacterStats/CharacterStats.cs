﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{

    [Serializable]
    public class CharacterStats
    {
        public string Name { get; }
        public string DisplayName { get; set; }

        public string PortraitPath;

        public Strength Strength { get; set; }
        public Dexterity Dexterity { get; set; }
        public Durability Durability { get; set; }
        public Charisma Charisma { get; set; }
        public Speed Speed { get; set; }
        public Intelligence Intelligence { get; set; }
        public Wisdom Wisdom { get; set; }

        public Attack Attack { get; set; }
        public Defense Defense { get; set; }


        public Dictionary<Type, IStat> Stats;
        public Dictionary<DamageType,DamageResistance> Resistances = new Dictionary<DamageType, DamageResistance>();
        public List<EquipmentSlotType> EquipmentSlotsTypes;

        public CharacterStats(string name, string portraitPath, Dictionary<Type,float> stats, Dictionary<DamageType, DamageResistance> resistances = null, EquipmentSlotType[] equipmentSlots = null)
        {
            Name = name;
            DisplayName = name;
            PortraitPath = portraitPath;

            Strength = new Strength(stats[typeof(Strength)]);
            Dexterity = new Dexterity(stats[typeof(Dexterity)]);
            Durability = new Durability(stats[typeof(Durability)]);
            Charisma = new Charisma(stats[typeof(Charisma)]);
            Speed = new Speed(stats[typeof(Speed)]);
            Intelligence = new Intelligence(stats[typeof(Intelligence)]);
            Wisdom = new Wisdom(stats[typeof(Wisdom)]);

            Attack = new Attack(0f, Strength);
            Defense = new Defense(0f, Dexterity);
            Resistances = resistances;

            EquipmentSlotsTypes = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();
            Stats = new Dictionary<Type, IStat>();
            var properties = typeof(CharacterStats).GetProperties();
            foreach(var property in properties)
            {
                if (typeof(IStat).IsAssignableFrom(property.PropertyType)) 
                    Stats.Add(property.PropertyType, (IStat)property.GetValue(this));

            }
        }


    }
}
