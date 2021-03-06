using Data.Shapes;
using System;

namespace Data.Items
{
    [Serializable]
    public class ItemStats: IItemStats
    {
        public string Id { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }

        public Rarity Rarity { get; }
        public float Weight { get; }
        public float GoldValue { get; }
        
        public string Description { get; }

        public string SpritePath { get; }
        public bool IsQuestItem { get; }

        public ItemStats(string itemId, float weight, float goldValue, string description, string spritePath, ShapeType shape = ShapeType.Monomino, Facing defaultFacing = Facing.Right, Rarity rarity = Rarity.common, bool isQuest = false)
        {
            Id = itemId;
            ShapeType = shape;
            DefaultFacing = defaultFacing;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
            SpritePath = spritePath;
            Rarity = rarity;
            IsQuestItem = isQuest;
        }
    }
}
