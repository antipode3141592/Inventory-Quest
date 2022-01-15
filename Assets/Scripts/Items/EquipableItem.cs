using InventoryQuest;
using InventoryQuest.Shapes;
using System;

namespace Data

{
    public class EquipableItem : IItem, IEquippable
    {
        public EquipableItem(ShapeType shape, ItemStats itemStats)
        {

        }

        public string Id { get; }

        public string Name { get; }

        public float Weight => throw new NotImplementedException();

        public float Value => throw new NotImplementedException();

        public void Equip()
        {
            throw new NotImplementedException();
        }
    }
}
