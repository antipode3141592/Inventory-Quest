using Data.Health;
using Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Characters
{
    //characters 
    public class PlayableCharacter : ICharacter
    {
        int weaponProficiencyIndex = 0;

        public string GuId { get; }
        public string DisplayName { get; set; }
        public int CurrentHealth { get; set; }
        public int HealthPerLevel { get; } = 5;
        public int MaximumHealth => StatDictionary[StatTypes.Vitality].CurrentValue + (HealthPerLevel * CurrentLevel);
        public int MagicPerLevel { get; } = 5;
        public int CurrentMagicPool { get; set; }
        public int MaximumMagicPool => StatDictionary[StatTypes.Arcane].CurrentValue + StatDictionary[StatTypes.Spirit].CurrentValue + (MagicPerLevel * CurrentLevel);
        public float MaximumEncumbrance => StatDictionary[StatTypes.Strength].CurrentValue * 15;
        public int CurrentExperience { get; set; }
        public int NextLevelExperience => (CurrentLevel ^ 2) * 250 + CurrentLevel * 750;
        public int CurrentLevel { get; set; }

        public IDictionary<DamageType, DamageResistance> Resistances { get; }
        public IDictionary<StatTypes, IStat> StatDictionary { get; }
        public IDictionary<string, EquipmentSlot> EquipmentSlots { get; }
        public IList<IWeaponProficiency> WeaponProficiencies { get; } = new List<IWeaponProficiency>();
        public IWeaponProficiency CurrentWeaponProficiency { get; protected set; }

        public ICharacterStats Stats { get; }

        public ContainerBase Backpack => (ContainerBase)EquipmentSlots.Values.FirstOrDefault(x => x.SlotType == EquipmentSlotType.Backpack).EquippedItem;
        public float CurrentEncumbrance => EquipmentSlots
            .Where(x => x.Value.EquippedItem is not null)
            .Sum(x => (x.Value.EquippedItem as IItem).Weight);
        public bool IsIncapacitated => CurrentHealth <= 0;

        public event EventHandler OnStatsUpdated;
        public event EventHandler<string> OnItemAddedToBackpack;

        public PlayableCharacter(ICharacterStats characterStats, IList<IEquipable> initialEquipment, IList<IItem> initialInventory = null)
        {
            GuId = Guid.NewGuid().ToString();
            DisplayName = characterStats.Name;
            Stats = characterStats;
            EquipmentSlots = new Dictionary<string, EquipmentSlot>();
            StatDictionary = new Dictionary<StatTypes, IStat>();

            //
            var initialStats = characterStats.InitialStats;
            var baseStats = characterStats.SpeciesBaseStats.BaseStats;

            //base stats
            var strength = new Strength(
                initialStats.ContainsKey(StatTypes.Strength) ? baseStats[StatTypes.Strength] + initialStats[StatTypes.Strength] : baseStats[StatTypes.Strength]);
            var vitality = new Vitality(
                initialStats.ContainsKey(StatTypes.Vitality) ? baseStats[StatTypes.Vitality] + initialStats[StatTypes.Vitality] : baseStats[StatTypes.Vitality]);
            var agility = new Agility(
                initialStats.ContainsKey(StatTypes.Agility) ? baseStats[StatTypes.Agility] + initialStats[StatTypes.Agility] : baseStats[StatTypes.Agility]);
            var speed = new Speed(
                initialStats.ContainsKey(StatTypes.Speed) ? baseStats[StatTypes.Speed] + initialStats[StatTypes.Speed] : baseStats[StatTypes.Speed]);
            var charisma = new Charisma(
                initialStats.ContainsKey(StatTypes.Charisma) ? baseStats[StatTypes.Charisma] + initialStats[StatTypes.Charisma] : baseStats[StatTypes.Charisma]);
            var intellect = new Intellect(
                initialStats.ContainsKey(StatTypes.Intellect) ? baseStats[StatTypes.Intellect] + initialStats[StatTypes.Intellect] : baseStats[StatTypes.Intellect]);
            var spirit = new Spirit(
                initialStats.ContainsKey(StatTypes.Spirit) ? baseStats[StatTypes.Spirit] + initialStats[StatTypes.Spirit] : baseStats[StatTypes.Spirit]);
            var arcane = new Arcane(
                initialStats.ContainsKey(StatTypes.Arcane) ? baseStats[StatTypes.Arcane] + initialStats[StatTypes.Arcane] : baseStats[StatTypes.Arcane]);

            //derived stats
            var attack = new Attack(
                initialStats.ContainsKey(StatTypes.Attack) ? initialStats[StatTypes.Attack] : 0,
                new CharacterStat[] { strength, speed });
            var defense = new Defense(
                initialStats.ContainsKey(StatTypes.Defense) ? initialStats[StatTypes.Defense] : 0,
                new CharacterStat[] { agility, vitality });
            var initiative = new Initiative(
                initialStats.ContainsKey(StatTypes.Initiative) ? initialStats[StatTypes.Initiative] : 0,
                new CharacterStat[] { agility, speed });
            var arcaneAffinity = new ArcaneAffinity(
                initialStats.ContainsKey(StatTypes.ArcaneAffinity) ? initialStats[StatTypes.ArcaneAffinity] : 0,
                new CharacterStat[] { intellect, arcane });
            var spiritAffinity = new SpiritAffinity(
                initialStats.ContainsKey(StatTypes.SpiritAffinity) ? initialStats[StatTypes.SpiritAffinity] : 0,
                new CharacterStat[] { charisma, spirit });

            //skills
            var climb = new Climb(
                initialStats.ContainsKey(StatTypes.Climb) ? initialStats[StatTypes.Climb] : 0);
            var swim = new Swim(initialStats.ContainsKey(StatTypes.Swim) ? initialStats[StatTypes.Swim] : 0);
            var persuade = new Persuade(initialStats.ContainsKey(StatTypes.Persuade) ? initialStats[StatTypes.Persuade] : 0);
            var intimidate = new Intimidate(initialStats.ContainsKey(StatTypes.Intimidate) ? initialStats[StatTypes.Intimidate] : 0);

            StatDictionary.Add(strength.Id, strength);
            StatDictionary.Add(vitality.Id, vitality);
            StatDictionary.Add(agility.Id, agility);
            StatDictionary.Add(speed.Id, speed);
            StatDictionary.Add(charisma.Id, charisma);
            StatDictionary.Add(intellect.Id, intellect);
            StatDictionary.Add(spirit.Id, spirit);
            StatDictionary.Add(arcane.Id, arcane);

            StatDictionary.Add(attack.Id, attack);
            StatDictionary.Add(defense.Id, defense);
            StatDictionary.Add(initiative.Id, initiative);
            StatDictionary.Add(arcaneAffinity.Id, arcaneAffinity);
            StatDictionary.Add(spiritAffinity.Id, spiritAffinity);

            StatDictionary.Add(climb.Id, climb);
            StatDictionary.Add(swim.Id, swim);
            StatDictionary.Add(persuade.Id, persuade);
            StatDictionary.Add(intimidate.Id, intimidate);

            foreach (EquipmentSlotType slotType in characterStats.EquipmentSlotsTypes)
            {
                AddEquipmentSlot(slotType);
            }

            foreach (var prof in characterStats.WeaponProficiencies)
            {
                foreach (var slotType in prof.EquipmentSlots)
                {
                    AddEquipmentSlot(slotType);
                }
                WeaponProficiencies.Add(prof);
            }
            weaponProficiencyIndex = 0;
            CurrentWeaponProficiency = WeaponProficiencies.Count > 0 ? WeaponProficiencies[weaponProficiencyIndex] : null;

            if (initialEquipment is null) return;
            foreach (IEquipable item in initialEquipment)
            {
                //iterate through slots, equip to first available valid placement for each piece of equipment
                foreach (var slot in EquipmentSlots)
                {
                    if (slot.Value.IsValidPlacement(item))
                    {
                        if (slot.Value.TryEquip(out _, item))
                            break;
                    }
                }
            }

            //if backpack present (some characters have no backpack), subscribe to container events
            if (Backpack is null) return;
            SubscribeToBackpackEvents();

            // add any initial Inventory to backpack
            if (initialInventory is null) return;
            foreach (IItem item in initialInventory)
            {
                ItemPlacementHelpers.TryAutoPlaceToContainer(container: Backpack, item: item); //no special failure path for now
            }

            void AddEquipmentSlot(EquipmentSlotType slotType)
            {
                int matchCount = EquipmentSlots.Count(x => x.Value.SlotType == slotType);
                string id = slotType.ToString().ToLower();
                if (matchCount > 0)
                    id += $"_{matchCount}";

                var slot = new EquipmentSlot(slotType, id);
                EquipmentSlots.Add(key: slot.Id, value: slot);
                slot.OnEquip += OnEquipHandler;
                slot.OnUnequip += OnUnequipHandler;
            }
        }

        //equipping functions
        public void OnEquipHandler(object sender, ModifierEventArgs e)
        {
            //Debug.Log($"OnEquipHandler: {sender} with {e.Modifiers.Count} modifiers");
            foreach (StatModifier mod in e.Modifiers)
            {
                ApplyModifier(mod);
            }
            OnStatsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void OnUnequipHandler(object sender, ModifierEventArgs e)
        {
            //Debug.Log($"OnUnEquipHandler: {sender}");
            foreach (var mod in e.Modifiers)
            {
                RemoveModifier(mod);
            }
            OnStatsUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void OnBackpackContentsChangedHandler(object sender, string e)
        {
            OnStatsUpdated?.Invoke(this, EventArgs.Empty);
        }


        //applying modifiers
        void ApplyModifier(StatModifier mod)
        {
            switch (mod.OperatorType)
            {
                case OperatorType.Add:
                    StatDictionary[mod.StatType].Modifier += mod.AdjustmentValue;
                    break;
                case OperatorType.Multiply:
                    StatDictionary[mod.StatType].Modifier *= mod.AdjustmentValue;
                    break;
                default:
                    break;
            }
        }

        void RemoveModifier(StatModifier mod)
        {
            switch (mod.OperatorType)
            {
                case OperatorType.Add:
                    StatDictionary[mod.StatType].Modifier -= mod.AdjustmentValue;
                    break;
                case OperatorType.Multiply:
                    StatDictionary[mod.StatType].Modifier /= mod.AdjustmentValue;
                    break;
                default:
                    break;
            }
        }

        public void ChangeToNextWeapon()
        {
            CurrentWeaponProficiency = WeaponProficiencies[weaponProficiencyIndex++%WeaponProficiencies.Count];

        }

        void SubscribeToBackpackEvents()
        {
            Backpack.OnItemPlaced += OnBackpackContentsChangedHandler;
            Backpack.OnItemTaken += OnBackpackContentsChangedHandler;
        }

        void UnsubscribeToBackpackEvents()
        {
            Backpack.OnItemPlaced -= OnBackpackContentsChangedHandler;
            Backpack.OnItemTaken -= OnBackpackContentsChangedHandler;
        }
    }
}
