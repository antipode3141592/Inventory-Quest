using Data.Shapes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items

{
    public class Item : IItem
    {
        public Item(IItemStats itemStats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = itemStats.Id;
            Stats = itemStats;
            Shape = ShapeFactory.GetShape(itemStats.ShapeType, itemStats.DefaultFacing);
            Sprite = Resources.Load<Sprite>(itemStats.SpritePath);
            Quantity = 1;
            Components = new();
        }

        public string GuId { get; }
        public string Id { get; }

        public Shape Shape { get; }

        public Sprite Sprite { get; set; }

        public Rarity Rarity { get; }

        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;

        public int Quantity {get; }

        public List<IItemComponent> Components { get; }

        public IItemStats Stats { get; }
    }
}
