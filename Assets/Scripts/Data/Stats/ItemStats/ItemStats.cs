using Data.Interfaces;
using System;

namespace Data
{
    [Serializable]
    public class ItemStats: IItemStats
    {
        public string Name { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }
        public float Weight { get; }
        public float GoldValue { get; }
        
        public string Description { get; }

        public ItemStats(string itemId, float weight, float goldValue, string description, ShapeType shape = ShapeType.Square1, Facing defaultFacing = Facing.Right)
        {
            Name = itemId;
            ShapeType = shape;
            DefaultFacing = defaultFacing;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
        }
    }
}
