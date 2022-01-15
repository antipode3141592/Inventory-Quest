using InventoryQuest;
using InventoryQuest.Shapes;
using System;
using UnityEngine;

namespace Data

{
    public class Item: IItem
    {
        public string Id { get; }
        public string Name { get; }
        public ItemStats Stats;
        public Shape Shape;

        public Item(ItemStats itemStats, Shape itemShape)
        {
            Id = Guid.NewGuid().ToString();
            Name = itemStats.ItemId;
            Stats = itemStats;
            Shape = itemShape;
        }

        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;
    }


}
