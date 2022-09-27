using Data.Characters;

namespace Data.Quests

{
    public interface IQuest
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }

        public string RewardId { get; }

        public IQuestStats Stats { get; }

        public bool Evaluate(Party party);

        public void Process(Party party);
    }
}