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
        public ItemStats ItemStats;
        public Shape ItemShape;

        public Item(string name, ItemStats itemStats, Shape itemShape)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            ItemStats = itemStats;
            ItemShape = itemShape;
        }



        //public virtual void Discard();
        //public virtual void Pickup();
    }
}
