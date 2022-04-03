using Data.Interfaces;

namespace Data
{
    public class ContainerStats : IItemStats, IContainerStats
    {
        

        public string Id { get; }

        public string Description { get; }

        public ShapeType ShapeType { get; }

        public Facing DefaultFacing { get; }

        public Rarity Rarity { get; }

        public float Weight { get; }

        public float GoldValue { get; }

        public Coor ContainerSize { get; }

        

        public string SpritePath { get; }
        
        public bool IsQuestItem { get; }

        public ContainerStats(string itemId, float weight, float goldValue, string description, string spritePath, Coor containerSize, ShapeType shape = ShapeType.Monomino, Facing defaultFacing = Facing.Right, Rarity rarity = Rarity.common, bool isQuestItem = false)
        {
            Id = itemId;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
            SpritePath = spritePath;
            ContainerSize = containerSize;
            ShapeType = shape;
            DefaultFacing = defaultFacing;
            Rarity = rarity;
            IsQuestItem = isQuestItem;
        }
    }
}
