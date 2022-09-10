using System;

namespace Data.Quests
{

    //ex:  Gather five schnozberries, Gather a Superior Quality Amberlin Crystal, etc.
    public class GatheringQuestStats : IGatheringQuestStats
    {
        public GatheringQuestStats(string id, string name, string description, int experience, string rewardId, string sourceId, QuestSourceTypes sourceType, string sinkId, QuestSourceTypes sinkType, int targetQuantity, string targetItemId)
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
            TargetQuantity = targetQuantity;
            TargetItemId = targetItemId;
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


        public int TargetQuantity { get; }
        public string TargetItemId { get; }


    }
}
