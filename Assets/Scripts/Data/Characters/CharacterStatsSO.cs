﻿using Data.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    [CreateAssetMenu(menuName = "InventoryQuest/Characters/CharacterStats", fileName = "c_")]
    public class CharacterStatsSO : SerializedScriptableObject, ICharacterStats
    {
        [SerializeField]  string id;
        [SerializeField]  string _name;
        [SerializeField]  string portraitPath;
        [SerializeField]  SpeciesBaseStatsSO species;

        [SerializeField]  Dictionary<StatTypes, int> initialStats;
        [SerializeField]  Dictionary<DamageType, DamageResistance> resistances;

        public string Id => id;
        public string Name => _name;
        public string PortraitPath => portraitPath;
        public string SpeciesId => species.Id;

        public IDictionary<StatTypes, int> InitialStats => initialStats;
        public IDictionary<DamageType, DamageResistance> Resistances => resistances;
        public IList<EquipmentSlotType> EquipmentSlotsTypes => species.SlotTypes;
    }
}