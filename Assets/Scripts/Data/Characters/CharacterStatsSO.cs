using Data.Health;
using Data.Items;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    [CreateAssetMenu(menuName = "InventoryQuest/Characters/CharacterStats", fileName = "c_")]
    public class CharacterStatsSO : SerializedScriptableObject, ICharacterStats
    {
        [SerializeField]  string id;
        [SerializeField]  string _name;
        [SerializeField, PreviewField]  Sprite portrait;
        [SerializeField]  SpeciesBaseStatsSO species;

        [SerializeField]  Dictionary<StatTypes, int> initialStats = new();
        [SerializeField]  Dictionary<DamageType, DamageResistance> resistances = new();
        [SerializeField]  List<IWeaponProficiency> weaponProficiencies = new();

        [SerializeField] List<IItemStats> startingEquipment;
        [SerializeField] List<Tuple<IItemStats, int>> startingInventory;

        public string Id => id;
        public string Name => _name;
        public Sprite Portrait => portrait;
        public string SpeciesId => species.Id;
        public ISpeciesBaseStats SpeciesBaseStats => species;

        public IDictionary<StatTypes, int> InitialStats => initialStats;
        public IDictionary<DamageType, DamageResistance> Resistances => resistances;
        public IList<EquipmentSlotType> EquipmentSlotsTypes => species.SlotTypes;

        public IList<IWeaponProficiency> WeaponProficiencies => weaponProficiencies;

        public List<IItemStats> StartingEquipment => startingEquipment;
        public List<Tuple<IItemStats,int>> StartingInventory => startingInventory;
    }
}
