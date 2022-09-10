using System;

namespace Data.Quests
{
    public class EscortQuestStats : IEscortQuestStats
    {
        public EscortQuestStats(string id, string name, string description, int experience, string rewardId, string sourceId, QuestSourceTypes sourceType, string sinkId, QuestSourceTypes sinkType, string escortCharacterId, string targetLocationId)
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

        public QuestSourceTypes SourceType { get; }

        public string SinkId { get; }

        public QuestSourceTypes SinkType { get; }

        #endregion

        public string EscortCharacterId { get; }

        public string TargetLocationId { get; }
    }
}
