namespace InventoryQuest
{
    public interface IQuest
    {
        public string Id { get; }
        public string Description { get; }

        public string RewardId { get; }

        public bool Evaluate(Party party);

        public void Cancel();
    }
}