namespace Data.Rewards
{
    public class RewardFactory
    {
        public static IReward GetReward(IRewardStats rewardStats)
        {
            var experienceStats = rewardStats as ItemRewardStats;
            if (experienceStats is not null) 
                return new ItemReward(experienceStats);

            return null;
        }
    }
}
