using Data.Characters;

namespace Data.Quests
{
    public abstract class Quest : IQuest
    {
        protected Quest(IQuestStats stats)
        {
            Id = stats.Id;
            Name = stats.Name;
            Description = stats.Description;
            RewardId = stats.RewardId;
            Stats = stats;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string RewardId { get; }

        public IQuestStats Stats { get; }

        public abstract bool Evaluate(Party party);
        public abstract void Process(Party party);
    }
}
