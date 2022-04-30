using System;

namespace Data.Quests

{
    public interface IQuestStats
    {
        public string Id { get; }

        public string Name { get; }
        public string Description { get; }

        public int Experience { get; }

        public string RewardId { get; }

        public string SourceId { get;  }

        public Type SourceType { get; } //could be a location trigger, character dialogue, or other game event
        
        public string SinkId { get; }

        public Type SinkType { get; }
    }
}
