using Data.Shapes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items

{
    public class Item : IItem
    {
        int quantity;

        public string GuId { get; }
        public string Id { get; }
        public IShape Shape { get; }
        public Facing CurrentFacing { get; protected set;}
        public Sprite Sprite { get; set; }
        public Rarity Rarity { get; }
        public float Weight => CalculateWeight();
        public float Value => Stats.GoldValue;
        public int Quantity 
        {
            get => quantity;
            set
            {
                if (value < Stats.MaxQuantity)
                    quantity = value;
                if (value == 0)
                    RequestDestruction?.Invoke(this, EventArgs.Empty);
            }
        }
        public IDictionary<Type, IItemComponent> Components { get; }
        public IItemStats Stats { get; }

        public event EventHandler RequestDestruction;

        public Item(IItemStats itemStats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = itemStats.Id;
            Stats = itemStats;
            Shape = itemStats.Shape;
            CurrentFacing = itemStats.DefaultFacing;
            Sprite = itemStats.PrimarySprite;
            Quantity = 1;
            Components = new Dictionary<Type, IItemComponent>();
        }

        public void Rotate(int direction)
        {
            int v = (int)CurrentFacing + direction;

            CurrentFacing = v % Shape.Points.Count < 0 ? 
                (Facing)(Shape.Points.Count - 1) : (Facing)(v % Shape.Points.Count);
        }

        protected float CalculateWeight()
        {
            if (!Components.ContainsKey(typeof(IContainer)))
                return Stats.Weight;
            return (Components[typeof(IContainer)] as IContainer).Weight;
        }
    }
}
