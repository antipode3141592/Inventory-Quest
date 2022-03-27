using System;
using System.Collections.Generic;

namespace Data.Rewards
{
    public class RewardDataSourceTest : IRewardDataSource
    {
        Dictionary<string, IRewardStats> rewardDictionary = new()
        {
            { "ring_charisma", new ItemRewardStats(
                id: "ring_charisma",
                name: "Ring of Charisma",
                description: "A simple silver band that is quite charming.",
                itemId: "ring_charisma_5"
                )}
        };

        public IRewardStats GetRewardById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
