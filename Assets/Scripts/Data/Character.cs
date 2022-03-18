using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Data
{
    //characters 
    public class Character: IDisposable
    {
        public string GuId { get; }

        public CharacterStats Stats;

        public Dictionary<EquipmentSlotType,EquipmentSlot> EquipmentSlots;

        public Container PrimaryContainer { get; set; }

        public EventHandler OnStatsUpdated;


        public Character(CharacterStats characterStats, Container primaryContainer)
        {
            GuId = Guid.NewGuid().ToString();
            PrimaryContainer = primaryContainer;
            Stats = characterStats;
            EquipmentSlots = new Dictionary<EquipmentSlotType,EquipmentSlot>();
            foreach (EquipmentSlotType slotType in characterStats.EquipmentSlotsTypes) 
            {
                var slot = new EquipmentSlot(slotType);
                EquipmentSlots.Add(key: slotType, value: slot);
                slot.OnEquip += OnEquipHandler;
                slot.OnUnequip += OnUnequipHandler;
            }
            PrimaryContainer.OnItemPlaced += OnContainerChangedHandler;
            PrimaryContainer.OnItemTaken += OnContainerChangedHandler;
        }

        public float CurrentEncumbrance => PrimaryContainer.TotalWeight + EquipmentSlots.Where(x => x.Value.EquippedItem is not null).Sum(x => x.Value.EquippedItem.Weight);

        public int GetItemCountById(string id)
        {
            var count = 0;
            count += PrimaryContainer.Contents.Count(x => x.Value.Item.Id == id);
            count += EquipmentSlots.Count(x => x.Value.EquippedItem != null && x.Value.EquippedItem.Id == id);
            return count;
        }

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
            Debug.Log($"OnEquipHandler: {sender} with {e.Modifiers.Count} modifiers");
            foreach(StatModifier mod in e.Modifiers)
            {
                ApplyModifier(mod);
            }
            OnStatsUpdated?.Invoke(this, EventArgs.Empty);
        }

        void ApplyModifier(StatModifier mod)
        {
            Type t = Stats.GetType();
            PropertyInfo prop = t.GetProperty(mod.StatType.Name);
            var obj = prop.GetValue(Stats);
            if (obj == null) return;
            PropertyInfo prop2 = obj.GetType().GetProperty("Modifier");
            var currentValue = prop2.GetValue(obj, null);

            if (mod.OperatorType == OperatorType.Add)
            {
                prop2.SetValue(obj, (int)currentValue + mod.AdjustmentValue);
            }
            else if (mod.OperatorType == OperatorType.Multiply)
            {
                prop2.SetValue(obj, (int)currentValue * mod.AdjustmentValue);
            }
        }

        public void OnUnequipHandler(object sender, ModifierEventArgs e)
        {
            Debug.Log($"OnUnEquipHandler: {sender}");
            foreach (var mod in e.Modifiers)
            {
                RemoveModifier(mod);
            }
            OnStatsUpdated?.Invoke(this, EventArgs.Empty);
        }

        void RemoveModifier(StatModifier mod)
        {
            Type t = Stats.GetType();
            PropertyInfo prop = t.GetProperty(mod.StatType.Name);
            var obj = prop.GetValue(Stats);
            if (obj == null) return;
            PropertyInfo prop2 = obj.GetType().GetProperty("Modifier");
            var currentValue = prop2.GetValue(obj, null);
            if (mod.OperatorType == OperatorType.Add)
            {
                prop2.SetValue(obj, (int)currentValue - mod.AdjustmentValue);
            }
            else if (mod.OperatorType == OperatorType.Multiply)
            {
                prop2.SetValue(obj, (int)currentValue * mod.AdjustmentValue);
            }
        }

        public void OnContainerChangedHandler(object sender, EventArgs e)
        {
            OnStatsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
