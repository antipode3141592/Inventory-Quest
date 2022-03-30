namespace Data.Rewards
{
    public class ItemRewardStats: IRewardStats
    {
        public ItemRewardStats(string id, string name, string description, string itemId)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemId = itemId;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string ItemId { get; }
    }
}
