﻿using Data.Interfaces;
using System;

namespace Data

{
    public class Item: IItem
    {
        public string GuId { get; }
        public string Id { get; }

        public Shape Shape { get; }

        public Item(IItemStats itemStats)
        {
            GuId = Guid.NewGuid().ToString();
            Id = itemStats.Id;
            Stats = itemStats;
            Shape = ShapeFactory.GetShape(itemStats.ShapeType, itemStats.DefaultFacing);
        }

        public float Weight => Stats.Weight;
        public float Value => Stats.GoldValue;

        public IItemStats Stats { get; }
    }
}
