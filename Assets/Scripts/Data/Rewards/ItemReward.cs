namespace Data.Rewards
{

    public class ItemReward : IReward
    {
        public ItemReward(ItemRewardStats stats)
        {
            Id = stats.Id;
            Name = stats.Name;
            Description = stats.Description;
            ItemId = stats.ItemId;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string ItemId { get; }
    }

    public class ItemRewardStats: IRewardStats
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string ItemId { get; }
    }
}
