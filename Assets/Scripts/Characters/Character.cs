using Data;
using System.Collections.Generic;
using System.Linq;

namespace InventoryQuest.Characters
{
    //characters 
    public class Character
    {
        public CharacterStats Stats;
        private List<StatModifier> _modifiers;

        public List<StatModifier> CurrentModifiers 
        {
            get
            {
                _modifiers.Clear();
                foreach(var slot in EquipmentSlots)
                {
                    _modifiers.AddRange(slot.Value.EquippedItem.Modifiers);
                }
                return _modifiers;
            }
        }

        public Dictionary<EquipmentSlotType,EquipmentSlot> EquipmentSlots;

        public Container PrimaryContainer { get; set; }

        public Character(CharacterStats characterStats, ContainerStats containerStats)
        {
            PrimaryContainer = ContainerFactory.GetContainer(containerStats);
            Stats = characterStats;
            _modifiers = new List<StatModifier>();
            EquipmentSlots = new Dictionary<EquipmentSlotType,EquipmentSlot>();
            foreach (EquipmentSlotType slotType in characterStats.EquipmentSlots) 
            { 
                EquipmentSlots.Add(key: slotType, value: new EquipmentSlot(slotType));
            }

        }

        //derived stats
        public float MaxEncumbrance => Stats.Strength.CurrentValue * 10f;
        public float MaxHealth => Stats.Durability.CurrentValue * 10f;
        public float CurrentEncumbrance => PrimaryContainer.TotalWeight;
        public float CurrentTotalGoldValue => PrimaryContainer.TotalWorth;

    }
}
