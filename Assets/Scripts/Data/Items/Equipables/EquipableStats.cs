using Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Items
{
    public class EquipableStats: IEquipableStats
    {

        public EquipableStats(EquipmentSlotType slotType, List<StatModifier> modifiers)
        {
            SlotType = slotType;
            Modifiers = modifiers;
        }

        public EquipmentSlotType SlotType { get; }

        public List<StatModifier> Modifiers { get; }

        
    }
}
