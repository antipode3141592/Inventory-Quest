using Data.Items;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    [CreateAssetMenu(menuName = "InventoryQuest/Characters/WeaponProficiency", fileName = "wp_")]
    public class WeaponProficiencySO : SerializedScriptableObject, IWeaponProficiency
    {
        [SerializeField] string _name;
        [SerializeField] IList<EquipmentSlotType> equipmentSlots;

        public string Name => _name;
        public IList<EquipmentSlotType> EquipmentSlots => equipmentSlots;
    }
}