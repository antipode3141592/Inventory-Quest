using Data.Interfaces;

namespace Data
{
    public class GatheringQuestStats: IQuestStats
    {
        public GatheringQuestStats(string id, string name, string description, int targetQuantity, string targetItemId, string rewardId)
        {
            Id = id;
            Name = name;
            Description = description;
            TargetQuantity = targetQuantity;
            TargetItemId = targetItemId;
            RewardId = rewardId;
        }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int TargetQuantity { get; }
        public string TargetItemId { get; }
        public string RewardId { get; }
    }
}
