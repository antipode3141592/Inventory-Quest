using Data.Shapes;

namespace Data.Items

{
    public class StackableItemStats: IItemStats
    {
        public StackableItemStats(string id, string description, ShapeType shapeType, float weight, float goldValue, string spritePath, int quantity = 1, int minStackSize = 4, Facing defaultFacing = Facing.Right, Rarity rarity = Rarity.common, bool isQuestItem = false)
        {
            Id = id;
            Description = description;
            ShapeType = shapeType;
            DefaultFacing = defaultFacing;
            Rarity = rarity;
            Weight = weight;
            GoldValue = goldValue;
            SpritePath = spritePath;
            IsQuestItem = isQuestItem;
            Quantity = quantity;
            MinStackSize = minStackSize;
        }

        public string Id { get; }
        public string Description { get; }
        public ShapeType ShapeType { get; }
        public Facing DefaultFacing { get; }
        public Rarity Rarity { get; }
        public float Weight { get; }
        public float GoldValue { get; }
        public string SpritePath { get; }
        public bool IsQuestItem { get; }
        public int Quantity { get; set; }
        public int MinStackSize { get; }
    }
}
