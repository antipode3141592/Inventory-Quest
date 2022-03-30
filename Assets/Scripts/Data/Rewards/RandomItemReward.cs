namespace Data.Rewards
{
    public class RandomItemReward : IReward
    {
        public RandomItemReward(RandomItemRewardStats stats)
        {
            Id = stats.Id;
            Name = stats.Name;
            Description = stats.Description;
            LootContainerId = stats.LootContainerId;
            LootTableId = stats.LootTableId;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string LootContainerId { get; }

        public string LootTableId { get; }


    }
}