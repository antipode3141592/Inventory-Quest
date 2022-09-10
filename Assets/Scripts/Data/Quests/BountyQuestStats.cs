using System;

namespace Data.Quests
{

    //  ex:  defeat five goblins
    public class BountyQuestStats : IBountyQuestStats
    {
        public BountyQuestStats(string id, string name, string description, int experience, string rewardId, string sourceId, QuestSourceTypes sourceType, string sinkId, QuestSourceTypes sinkType, int bountyTargetQuantity, string bountyTargetId)
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

        public int BountyTargetQuantity { get; }

        public string BountyTargetId { get; }
    }
}
