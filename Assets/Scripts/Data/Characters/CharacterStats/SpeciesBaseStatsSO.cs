using Data.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    [CreateAssetMenu(menuName = "InventoryQuest/Characters/BaseStats", fileName ="SpeciesStats_")]
    public class SpeciesBaseStatsSO: SerializedScriptableObject
    {
        public string Id;
        public Dictionary<StatTypes, int> BaseStats;
        public List<EquipmentSlotType> SlotTypes;
    }
}
