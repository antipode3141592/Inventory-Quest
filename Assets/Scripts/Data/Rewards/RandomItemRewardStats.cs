namespace Data.Rewards
{
    public class RandomItemRewardStats : IRewardStats
    {
        public RandomItemRewardStats(string id, string name, string description, string lootContainerId, string lootTableId)
        {
            Id = id;
            Name = name;
            Description = description;
            LootContainerId = lootContainerId;
            LootTableId = lootTableId;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string LootContainerId { get; }

        public string LootTableId { get; }
    }
}