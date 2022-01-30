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

        public Dictionary<EquipmentSlotType,EquipmentSlot> EquipmentSlots;

        public Container PrimaryContainer { get; set; }

        public Character(CharacterStats characterStats, ContainerStats containerStats)
        {
            PrimaryContainer = ContainerFactory.GetContainer(containerStats);
            Stats = characterStats;
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
            foreach(StatModifier mod in e.Modifiers)
            {
                ApplyModifier(mod);
            }
        }

        void ApplyModifier(StatModifier mod)
        {
            Type t = Stats.GetType();
            FieldInfo field = t.GetField(mod.StatType.Name.ToString());
            var obj = field.GetValue(Stats);
            if (obj == null) return;
            PropertyInfo prop2 = obj.GetType().GetProperty("Modifier");
            var currentValue = prop2.GetValue(obj, null);

            if (mod.OperatorType == OperatorType.Add)
            {
                prop2.SetValue(obj, (float)currentValue + mod.AdjustmentValue);
            }
            else if (mod.OperatorType == OperatorType.Multiply)
            {
                prop2.SetValue(obj, (float)currentValue * mod.AdjustmentValue);
            }
        }

        public void OnUnequipHandler(object sender, ModifierEventArgs e)
        {
            Debug.Log($"OnUnEquipHandler: {sender}");
            foreach (var mod in e.Modifiers)
            {
                RemoveModifier(mod);
            }
        }

        void RemoveModifier(StatModifier mod)
        {
            Type t = Stats.GetType();
            FieldInfo field = t.GetField(mod.StatType.Name.ToString());
            var obj = field.GetValue(Stats);
            if (obj == null) return;
            PropertyInfo prop2 = obj.GetType().GetProperty("Modifier");
            var currentValue = prop2.GetValue(obj, null);
            if (mod.OperatorType == OperatorType.Add)
            {
                prop2.SetValue(obj, (float)currentValue - mod.AdjustmentValue);
            }
            else if (mod.OperatorType == OperatorType.Multiply)
            {
                prop2.SetValue(obj, (float)currentValue / mod.AdjustmentValue);
            }
        }
    }
}
