﻿namespace Data
{
    public interface IItemStats
    {
        public string ItemId { get; }
        public string Description { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }
        public float Weight { get; }
        public float GoldValue { get; }
    }
}
