using System.Collections.Generic;

namespace Data

{
    public interface IEquipable
    {
        public EquipmentSlotType SlotType { get; }
        public List<StatModifier> Modifiers { get; set; }
        
    }
}
