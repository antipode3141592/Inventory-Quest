using Data.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    [CreateAssetMenu(menuName = "InventoryQuest/Characters/BaseStats", fileName = "SpeciesStats_")]
    public class SpeciesBaseStatsSO : SerializedScriptableObject, ISpeciesBaseStats
    {
        [SerializeField] string id;
        [SerializeField] Dictionary<StatTypes, int> baseStats;
        [SerializeField] List<EquipmentSlotType> slotTypes;

        public string Id => id;
        public Dictionary<StatTypes, int> BaseStats => baseStats;
        public List<EquipmentSlotType> SlotTypes => slotTypes;
    }
}
