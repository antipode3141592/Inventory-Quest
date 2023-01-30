using System.Collections.Generic;
using Data;
using Data.Items;
using Data.Shapes;
using UnityEngine;

namespace InventoryQuest.Testing.Stubs
{
    class ItemStats : IItemStats
    {
        public ItemStats(string id, string name, string description, IShape shape, Facing defaultFacing, Rarity rarity, float weight, float individualGoldValue, Sprite sprite, bool isQuestItem, IList<IItemComponentStats> components, IList<Tag> tags = null, int maxQuantity = 1, bool isStackable = false)
        {
            Id = id;
            Name = name;
            Description = description;
            Shape = shape;
            DefaultFacing = defaultFacing;
            Rarity = rarity;
            Weight = weight;
            IndividualGoldValue = individualGoldValue;
            PrimarySprite = sprite;
            IsQuestItem = isQuestItem;
            Components = components;
            Tags = tags;
            MaxQuantity = maxQuantity;
            IsStackable = isStackable;
        }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public IShape Shape { get; }
        public Facing DefaultFacing { get; }
        public Rarity Rarity { get; }
        public float Weight { get; }
        public Sprite PrimarySprite { get; }
        public bool IsQuestItem { get; }

        public IList<IItemComponentStats> Components { get; }
        public IEnumerable<Tag> Tags { get; }

        public float IndividualGoldValue { get; }

        public bool IsStackable { get; }

        public int MaxQuantity { get; }
    }
}
