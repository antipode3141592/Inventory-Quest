using System.Collections.Generic;
using Data;
using Data.Items;
using Data.Shapes;
using UnityEngine;

namespace InventoryQuest.Testing.Stubs
{
    class ItemStats : IItemStats
    {
        public ItemStats(string id, string description, IShape shape, Facing defaultFacing, Rarity rarity, float weight, float goldValue, Sprite sprite, bool isQuestItem, IList<IItemComponentStats> components, IList<Tag> tags = null)
        {
            Id = id;
            Description = description;
            Shape = shape;
            DefaultFacing = defaultFacing;
            Rarity = rarity;
            Weight = weight;
            GoldValue = goldValue;
            PrimarySprite = sprite;
            IsQuestItem = isQuestItem;
            Components = components;
            Tags = tags;
        }

        public string Id { get; }
        public string Description { get; }
        public IShape Shape { get; }
        public Facing DefaultFacing { get; }
        public Rarity Rarity { get; }
        public float Weight { get; }
        public float GoldValue { get; }
        public Sprite PrimarySprite { get; }
        public bool IsQuestItem { get; }

        public IList<IItemComponentStats> Components { get; }
        public IEnumerable<Tag> Tags { get; }
    }
}
