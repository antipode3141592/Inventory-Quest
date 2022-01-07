using InventoryQuest;
using InventoryQuest.Shapes;
using System;

namespace Data

{
    public class EquipableItem : Item, IEquippable
    {
        public EquipableItem(string id, ItemStats itemStats, Shape itemShape) : base(id, itemStats, itemShape)
        {

        }

        public void Equip()
        {
            throw new NotImplementedException();
        }
    }
}
