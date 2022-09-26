using Data.Characters;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class WeaponGroup: SerializedMonoBehaviour
    {
        [SerializeField] IWeaponProficiency weaponProficiency;
        [SerializeField] List<EquipmentSlotDisplay> weaponSlots;

        public List<EquipmentSlotDisplay> WeaponSlots => weaponSlots;

        public string WeaponProficiencyName => weaponProficiency.Name;
    }
}
