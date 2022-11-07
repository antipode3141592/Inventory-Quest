using Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data.Items
{
    [Serializable]
    public class EquipableStats : IEquipableStats
    {
        [SerializeField] EquipmentSlotType _slotType;
        [SerializeField] List<StatModifier> _modifiers;

        public EquipableStats(EquipmentSlotType slotType, List<StatModifier> modifiers)
        {
            _slotType = slotType;
            _modifiers = modifiers;
        }

        public EquipmentSlotType SlotType => _slotType;

        public List<StatModifier> Modifiers => _modifiers;

        
    }
}
