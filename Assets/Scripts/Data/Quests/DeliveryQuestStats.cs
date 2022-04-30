using System;
using System.Collections.Generic;

namespace Data.Quests
{
    public class DeliveryQuestStats : IQuestStats
    {
        public DeliveryQuestStats(string id, string name, string description, int experience, string rewardId, string sourceId, Type sourceType, string sinkId, Type sinkType, IList<(string,int)> deliveryItemIdsAndQuantities)
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
            DeliveryItemIdsAndQuantities = deliveryItemIdsAndQuantities;
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

        public IList<(string, int)> DeliveryItemIdsAndQuantities { get; }
    }
}