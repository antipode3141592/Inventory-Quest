using System;
using System.Collections.Generic;

namespace Data
{

    [Serializable]
    public class CharacterStats
    {
        public Strength Strength;
        public Dexterity Dexterity;
        public Durability Durability;
        public Charisma Charisma;
        public Speed Speed;
        public Intelligence Intelligence;
        public Wisdom Wisdom;

        public Attack Attack;


        public Dictionary<DamageType,DamageResistance> Resistances = new Dictionary<DamageType, DamageResistance>();
        public List<EquipmentSlotType> EquipmentSlotsTypes;

        public CharacterStats(Dictionary<Type,float> stats, Dictionary<DamageType, DamageResistance> resistances = null, EquipmentSlotType[] equipmentSlots = null)
        {
            Strength = new Strength(stats[typeof(Strength)]);
            Dexterity = new Dexterity(stats[typeof(Dexterity)]);
            Durability = new Durability(stats[typeof(Durability)]);
            Charisma = new Charisma(stats[typeof(Charisma)]);
            Speed = new Speed(stats[typeof(Speed)]);
            Intelligence = new Intelligence(stats[typeof(Intelligence)]);
            Wisdom = new Wisdom(stats[typeof(Wisdom)]);

            Attack = new Attack(0f, Strength);
            Resistances = resistances;

            EquipmentSlotsTypes = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();
        }


    }
}
