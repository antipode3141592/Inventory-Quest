namespace Data.Rewards
{
    public interface IReward
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }
    }
}
