using System;
using System.Collections.Generic;

namespace Data
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

        public List<EquipmentSlotType> EquipmentSlots;

        public CharacterStats(float strength, float dexterity, float durability, float lightingResistance = 0f, float acidResistance = 0f, float fireResistance = 0f, float iceResistance = 0f, EquipmentSlotType[] equipmentSlots = null)
        {
            Strength = new CharacterStat(strength);
            Dexterity = new CharacterStat(dexterity);
            Durability = new CharacterStat(durability);
            LightingResistance = new DamageResistance(DamageType.Lightning, lightingResistance);
            AcidResistance = new DamageResistance(DamageType.Acid, acidResistance);
            FireResistance = new DamageResistance(DamageType.Fire, fireResistance);
            IceResistance = new DamageResistance(DamageType.Ice, iceResistance);
            EquipmentSlots = equipmentSlots != null ? new List<EquipmentSlotType>(equipmentSlots) : new List<EquipmentSlotType>();
        }
    }
}
