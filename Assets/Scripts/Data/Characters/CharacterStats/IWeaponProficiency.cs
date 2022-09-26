using Data.Items;
using System.Collections.Generic;

namespace Data.Characters
{
    public interface IWeaponProficiency
    {
        public string Name { get; }
        public IList<EquipmentSlotType> EquipmentSlots { get; }
    }
}