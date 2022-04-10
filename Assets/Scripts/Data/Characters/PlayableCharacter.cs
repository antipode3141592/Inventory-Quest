using Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Data.Characters
{
    //characters 
    public class PlayableCharacter : IDisposable, ICharacter
    {
        public string GuId { get; }

        public CharacterStats Stats;

        public Dictionary<EquipmentSlotType, EquipmentSlot> EquipmentSlots;

        public IContainer Backpack => EquipmentSlots[EquipmentSlotType.Backpack].EquippedItem as IContainer;

        public EventHandler OnStatsUpdated;

        public PlayableCharacter(CharacterStats characterStats, IList<IEquipable> initialEquipment, IList<IItem> initialInventory = null)
        {
            GuId = Guid.NewGuid().ToString();
            Stats = characterStats;
            EquipmentSlots = new Dictionary<EquipmentSlotType, EquipmentSlot>();
            foreach (EquipmentSlotType slotType in characterStats.EquipmentSlotsTypes)
            {
                var slot = new EquipmentSlot(slotType);
                EquipmentSlots.Add(key: slotType, value: slot);
                slot.OnEquip += OnEquipHandler;
                slot.OnUnequip += OnUnequipHandler;
            }
            if (initialEquipment is null) return;
            foreach (IEquipable item in initialEquipment)
            {
                if (EquipmentSlots.ContainsKey(item.SlotType))
                    Debug.Log($"Equipping {(item as IItem).Id} to {item.SlotType}");
                EquipmentSlots[item.SlotType].TryEquip(out _, item);
            }

            if (Backpack is null) return;
            Backpack.OnItemPlaced += OnBackpackChangedHandler;
            Backpack.OnItemTaken += OnBackpackChangedHandler;
            // add initial Inventory to backpack
        }

        public float CurrentEncumbrance => EquipmentSlots.Where(x => x.Value.EquippedItem is not null).Sum(x => (x.Value.EquippedItem as IItem).Weight);

        public bool IsIncapacitated => Stats.CurrentHealth <= 0 ? true : false;

        public void Dispose()
        {
            foreach (var slot in EquipmentSlots)
            {
                slot.Value.OnEquip -= OnEquipHandler;
                slot.Value.OnUnequip -= OnUnequipHandler;
            }
        }

        public void OnEquipHandler(object sender, ModifierEventArgs e)
        {
            Debug.Log($"OnEquipHandler: {sender} with {e.Modifiers.Count} modifiers");
            foreach (StatModifier mod in e.Modifiers)
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

        public void OnBackpackChangedHandler(object sender, EventArgs e)
        {
            OnStatsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
