namespace Data.Rewards
{
    public class CharacterReward : IReward
    {
        public CharacterReward(CharacterRewardStats stats)
        {
            Id = stats.Id;
            Name = stats.Name;
            Description = stats.Description;
        }



        public string Id { get; }

        public string Name { get; }

        public string Description { get; }


    }

    public class CharacterRewardStats : IRewardStats
    {
        public CharacterRewardStats(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }
    }
}
