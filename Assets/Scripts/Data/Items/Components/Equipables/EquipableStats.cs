using Data.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items
{
    public class EquipableStats : IEquipableStats
    {
        [SerializeField] EquipmentSlotType _slotType;
        [SerializeField] List<StatModifier> _statModifiers = new();
        [SerializeField] List<ResistanceModifier> _resistanceModifiers = new();

        public EquipableStats(EquipmentSlotType slotType, List<StatModifier> statModifiers, List<ResistanceModifier> resistanceModifiers)
        {
            _slotType = slotType;
            _statModifiers = statModifiers;
            _resistanceModifiers = resistanceModifiers;
        }

        public EquipableStats()
        {
            _slotType = EquipmentSlotType.Ring;
            _statModifiers = new();
            _resistanceModifiers = new();
        }

        public EquipmentSlotType SlotType => _slotType;
        public List<StatModifier> StatModifiers => _statModifiers;
        public List<ResistanceModifier> ResistanceModifiers => _resistanceModifiers;
    }
}
