using System;
using System.Collections.Generic;

namespace Data.Rewards
{
    public class RewardDataSourceTest : IRewardDataSource
    {
        Dictionary<string, IRewardStats> rewardDictionary = new()
        {
            { "exp", new ItemRewardStats()}
        };

        public IRewardStats GetRewardById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
