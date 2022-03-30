namespace Data.Interfaces
{
    public interface IItemStats
    {
        public string Id { get; }
        public string Description { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }

        public Rarity Rarity { get; }
        public float Weight { get; }
        public float GoldValue { get; }

        public string SpritePath { get; }

        public bool IsQuest { get; }

    }
}
