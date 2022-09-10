using System.Collections.Generic;

namespace Data.Quests
{
    public class DeliveryQuestStats : IDeliveryQuestStats
    {
        public DeliveryQuestStats(string id, string name, string description, int experience, string rewardId, string sourceId, QuestSourceTypes sourceType, string sinkId, QuestSourceTypes sinkType, List<string> itemIdsToDeliver, List<int> quantitiesToDeliver)
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
            ItemIds = itemIdsToDeliver;
            Quantities = quantitiesToDeliver;
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

        public List<string> ItemIds { get; }
        public List<int> Quantities { get; }
    }
}