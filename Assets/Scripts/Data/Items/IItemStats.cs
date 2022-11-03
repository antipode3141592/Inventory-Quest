using Data.Shapes;
using System.Collections.Generic;

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
        public string SpritePath { get; }
        public bool IsQuestItem { get; }

        public IEnumerable<IItemComponentStats> Components { get; }
    }
}
