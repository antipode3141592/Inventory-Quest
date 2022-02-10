namespace Data.Interfaces
{
    public interface IQuest
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }

        public string RewardId { get; }

        public bool Evaluate();

        public void Cancel();
    }
}