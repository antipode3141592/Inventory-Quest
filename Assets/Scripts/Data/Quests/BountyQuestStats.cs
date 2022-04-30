using System;

namespace Data.Quests
{
    public class BountyQuestStats : IQuestStats
    {
        public BountyQuestStats(string id, string name, string description, int experience, string rewardId, string sourceId, Type sourceType, string sinkId, Type sinkType, int bountyTargetQuantity, string bountyTargetId, Type bountyTargetType)
        {
            Id = id;
            Name = name;
            Description = description;
            Experience = experience;
            RewardId = rewardId;
            SourceId = sourceId;
            SourceType = sourceType;
            SinkId = sinkId;
            SinkType = sinkType;
            BountyTargetQuantity = bountyTargetQuantity;
            BountyTargetId = bountyTargetId;
            BountyTargetType = bountyTargetType;
        }

        #region IQuestStats
        public string Id { get; }

        public string Name { get; }
        public string Description { get; }

        public int Experience { get; }

        public string RewardId { get; }

        public string SourceId { get; }

        public Type SourceType { get; }

        public string SinkId { get; }

        public Type SinkType { get; }
        #endregion

        public int BountyTargetQuantity { get; }

        public string BountyTargetId { get; }

        public Type BountyTargetType { get; }

    }
}
