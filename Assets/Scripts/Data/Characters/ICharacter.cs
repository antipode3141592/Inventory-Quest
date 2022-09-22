using Data.Items;
using System;
using System.Collections.Generic;

namespace Data.Characters
{
    public interface ICharacter
    {
        public string GuId { get; }
        public string DisplayName { get; set; }
        public IDictionary<DamageType, DamageResistance> Resistances { get; }
        public IDictionary<StatTypes, IStat> StatDictionary { get; }
        public IDictionary<string, EquipmentSlot> EquipmentSlots { get; }
        public ContainerBase Backpack { get; }

        public float CurrentEncumbrance { get; }
        public float MaximumEncumbrance { get; }

        public bool IsIncapacitated { get; }

        public int MaximumHealth { get; }
        public int MaximumMagicPool { get; }
        public int CurrentHealth { get; set; }
        public int CurrentMagicPool { get; set; }
        public int HealthPerLevel { get; }
        public int MagicPerLevel { get; }

        public int CurrentExperience { get; set; }
        public int NextLevelExperience { get; }
        public int CurrentLevel { get; set; }

        public ICharacterStats Stats { get; }

        //events
        public event EventHandler OnStatsUpdated;

        //event handlers
        public void OnBackpackChangedHandler(object sender, EventArgs e);
        public void OnEquipHandler(object sender, ModifierEventArgs e);
        public void OnUnequipHandler(object sender, ModifierEventArgs e);
    }
}