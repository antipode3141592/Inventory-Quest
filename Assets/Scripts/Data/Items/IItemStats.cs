using Data.Shapes;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items
{
    public interface IItemStats
    {
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
