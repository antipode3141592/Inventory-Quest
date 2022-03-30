namespace Data.Rewards
{
    public class RewardFactory
    {
        public static IReward GetReward(IRewardStats rewardStats)
        {
            var itemRewardStats = rewardStats as ItemRewardStats;
            if (itemRewardStats is not null) 
                return new ItemReward(itemRewardStats);
            var randomItemRewardStats = rewardStats as RandomItemRewardStats;
            if (randomItemRewardStats is not null)
                return new RandomItemReward(randomItemRewardStats);
            var characterRewardStats = rewardStats as CharacterRewardStats;
            if (characterRewardStats is not null)
                return new CharacterReward(characterRewardStats);
            return null;
        }
    }
}
