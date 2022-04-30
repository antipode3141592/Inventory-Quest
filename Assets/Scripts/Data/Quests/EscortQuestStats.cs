using System;

namespace Data.Quests
{
    public class EscortQuestStats : IQuestStats
    {
        public EscortQuestStats(string id, string name, string description, int experience, string rewardId, string sourceId, Type sourceType, string sinkId, Type sinkType, string escortCharacterId, string targetLocationId)
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
            EscortCharacterId = escortCharacterId;
            TargetLocationId = targetLocationId;
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

        public string EscortCharacterId { get; }

        public string TargetLocationId { get; }
    }
}
