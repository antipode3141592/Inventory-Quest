using UnityEngine;

namespace Data.Quests
{
    public abstract class QuestStatsSO : ScriptableObject, IQuestStats
    {
        [SerializeField] protected string id;
        [SerializeField] protected string _name;
        [SerializeField] protected int experience;
        [SerializeField, TextArea(4,10)] protected string description;
        [SerializeField] protected string rewardId;
        [SerializeField] protected string sourceId;
        [SerializeField] protected QuestSourceTypes sourceType;
        [SerializeField] protected string sinkId;
        [SerializeField] protected QuestSourceTypes sinkType;

        public string Id => id;
        public string Name => _name;
        public string Description => description;
        public int Experience => experience;
        public string RewardId => rewardId;
        public string SourceId => sourceId;
        public QuestSourceTypes SourceType => sourceType;
        public string SinkId => sinkId;
        public QuestSourceTypes SinkType => sinkType;
    }
}
