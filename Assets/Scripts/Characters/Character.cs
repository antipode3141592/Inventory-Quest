using Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace InventoryQuest.Characters
{
    //characters 
    public class Character: IDisposable
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
            foreach (EquipmentSlotType slotType in characterStats.EquipmentSlotsTypes) 
            {
                var slot = new EquipmentSlot(slotType);
                EquipmentSlots.Add(key: slotType, value: slot);
                slot.OnEquip += OnEquipHandler;
                slot.OnUnequip += OnUnequipHandler;
            }
        }

        

        //derived stats
        public float MaxEncumbrance => Stats.Strength.CurrentValue * 10f;
        public float MaxHealth => Stats.Durability.CurrentValue * 10f;
        public float CurrentEncumbrance => PrimaryContainer.TotalWeight;
        public float CurrentTotalGoldValue => PrimaryContainer.TotalWorth;

        public void Dispose()
        {
            foreach(var slot in EquipmentSlots)
            {
                slot.Value.OnEquip -= OnEquipHandler;
                slot.Value.OnUnequip -= OnUnequipHandler;
            }
        }

        public void OnEquipHandler(object sender, ModifierEventArgs e)
        {
            Debug.Log($"OnEquipHandler: {sender}");
            foreach(var mod in e.Modifiers)
            {
                Type t = Stats.GetType();
                FieldInfo field = t.GetField(mod.StatType.Name.ToString());
                var obj = field.GetValue(Stats);
                if (obj == null) continue;
                PropertyInfo prop2 = obj.GetType().GetProperty("CurrentValue");
                var currentValue = prop2.GetValue(obj,null);

                if (mod.OperatorType == OperatorType.Add)
                {
                    prop2.SetValue(obj, (float)currentValue + mod.AdjustmentValue);
                } else if (mod.OperatorType == OperatorType.Multiply)
                {
                    prop2.SetValue(obj, (float)currentValue * mod.AdjustmentValue);
                }
            }
        }

        public void OnUnequipHandler(object sender, ModifierEventArgs e)
        {
            Debug.Log($"OnUnEquipHandler: {sender}");
            foreach (var mod in e.Modifiers)
            {
                Type t = Stats.GetType();
                FieldInfo field = t.GetField(mod.StatType.Name.ToString());
                var obj = field.GetValue(Stats);
                if (obj == null) continue;
                PropertyInfo prop2 = obj.GetType().GetProperty("CurrentValue");
                var currentValue = prop2.GetValue(obj, null);
                if (mod.OperatorType == OperatorType.Add)
                {
                    prop2.SetValue(obj, (float)currentValue - mod.AdjustmentValue);
                }else if (mod.OperatorType == OperatorType.Multiply)
                {
                    prop2.SetValue(obj, (float)currentValue / mod.AdjustmentValue);
                }
            }
        }
    }
}
