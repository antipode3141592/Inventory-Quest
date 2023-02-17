using Data.Items;
using System;
using System.Collections.Generic;

namespace Data.Characters
{
    public interface ICharacter: IDamagable
    {
        public string GuId { get; }
        public string DisplayName { get; set; }
        public IDictionary<StatTypes, IStat> StatDictionary { get; }
        public IDictionary<string, EquipmentSlot> EquipmentSlots { get; }
        public IList<IWeaponProficiency> WeaponProficiencies { get; }
        public IWeaponProficiency CurrentWeaponProficiency { get; }
        public IContainer Backpack { get; }

        public float CurrentEncumbrance { get; }
        public float MaximumEncumbrance { get; }

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

        public bool IsIncapacitated { get; }
        public bool IsDying { get; }
        public bool IsDead { get; }

        //events
        public event EventHandler OnStatsUpdated;

        public event EventHandler<string> OnItemAddedToBackpack;

        public event EventHandler OnDying;
        public event EventHandler OnDead;

        //event handlers
        public void OnBackpackContentsChangedHandler(object sender, string e);
        public void OnEquipHandler(object sender, ModifierEventArgs e);
        public void OnUnequipHandler(object sender, ModifierEventArgs e);
        public void ChangeToNextWeapon();

        public void ApplyModifiers(IList<StatModifier> modifiers);
        public void RemoveModifiers(IList<StatModifier> modifiers);
    }
}