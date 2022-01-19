using InventoryQuest;
using System;

namespace Data

{
    public class Item: IItem
    {
        public string Id { get; }
        public string Name { get; }
        //public ItemStats Stats;
        public Shape Shape { get; }

        public Item(ItemStats itemStats)
        {
            Id = Guid.NewGuid().ToString();
            Name = itemStats.ItemId;
            Stats = itemStats;
            Shape = ShapeFactory.GetShape(itemStats.ShapeType);
        }

        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;

        public IItemStats Stats { get; }
    }


}
