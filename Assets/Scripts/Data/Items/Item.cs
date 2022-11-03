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
            Shape = itemStats.Shape;
            Sprite = Resources.Load<Sprite>(itemStats.SpritePath);
            Quantity = 1;
            Components = new Dictionary<Type, IItemComponent>();
        }

        public string GuId { get; }
        public string Id { get; }

        public IShape Shape { get; }

        public Facing CurrentFacing { get; protected set;}

        public Sprite Sprite { get; set; }

        public Rarity Rarity { get; }

        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;

        public int Quantity {get; }

        public IDictionary<Type, IItemComponent> Components { get; }

        public IItemStats Stats { get; }

        public void Rotate(int direction)
        {
            int v = (int)CurrentFacing + direction;

            CurrentFacing = v % Shape.Points.Count < 0 ? 
                (Facing)(Shape.Points.Count - 1) : (Facing)(v % Shape.Points.Count);
        }
    }
}
