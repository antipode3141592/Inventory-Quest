using Data.Shapes;
using System;
using UnityEngine;

namespace Data.Items

{
    public class Item: IItem
    {
        public Item(IItemStats itemStats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = itemStats.Id;
            Stats = itemStats;
            Shape = ShapeFactory.GetShape(itemStats.ShapeType, itemStats.DefaultFacing);
            Sprite = Resources.Load<Sprite>(itemStats.SpritePath);
        }

        public string GuId { get; }
        public string Id { get; }

        public Shape Shape { get; }

        public Sprite Sprite { get; set; }

        public Rarity Rarity { get; }

        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;

        public IItemStats Stats { get; }
    }
}
