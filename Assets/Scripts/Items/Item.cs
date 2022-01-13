using InventoryQuest;
using InventoryQuest.Shapes;
using System;
using UnityEngine;

namespace Data

{
    public class Item
    {
        [SerializeField]
        public string Id { get; }
        public string Name;
        public ItemStats Stats;
        public Shape Shape;
        public Container Container;

        public Item(string name, ItemStats itemStats, Shape itemShape, Container container = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Stats = itemStats;
            Shape = itemShape;
            Container = container;
        }
    }
}
