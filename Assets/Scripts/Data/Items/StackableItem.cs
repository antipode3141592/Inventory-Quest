using Data.Shapes;
using System;
using UnityEngine;

namespace Data.Items

{
    public class StackableItem: IItem, IStackable
    {
        public StackableItem(StackableItemStats stats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = stats.Id;
            Shape = ShapeFactory.GetShape(stats.ShapeType, stats.DefaultFacing);
            Sprite = Resources.Load<Sprite>(stats.SpritePath);
            Rarity = stats.Rarity;
            Stats = stats;
            Quantity = 1;
            MinStackSize = stats.MinStackSize;
        }

        public string GuId { get; }
        public string Id { get; }

        public Shape Shape { get; }

        public Sprite Sprite { get; set; }

        public Rarity Rarity { get; }
        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;

        public IItemStats Stats { get; }

        public int Quantity { get; set; }
        public int MinStackSize { get; }
    }
}
