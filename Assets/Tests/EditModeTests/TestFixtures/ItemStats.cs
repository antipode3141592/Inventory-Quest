using System.Collections.Generic;
using Data;
using Data.Items;
using Data.Shapes;

namespace InventoryQuest.Testing.Stubs
{
    class ItemStats : IItemStats
    {
        public ItemStats(string id, string description, IShape shape, Facing defaultFacing, Rarity rarity, float weight, float goldValue, string spritePath, bool isQuestItem, IList<IItemComponentStats> components)
        {
            Id = id;
            Description = description;
            Shape = shape;
            DefaultFacing = defaultFacing;
            Rarity = rarity;
            Weight = weight;
            GoldValue = goldValue;
            SpritePath = spritePath;
            IsQuestItem = isQuestItem;
            Components = components;
        }

        public string Id { get; }
        public string Description { get; }
        public IShape Shape { get; }
        public Facing DefaultFacing { get; }
        public Rarity Rarity { get; }
        public float Weight { get; }
        public float GoldValue { get; }
        public string SpritePath { get; }
        public bool IsQuestItem { get; }

        public IList<IItemComponentStats> Components { get; }
    }
}
