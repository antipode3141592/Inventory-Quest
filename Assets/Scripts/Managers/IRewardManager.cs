using Data.Rewards;
using System;

namespace InventoryQuest.Managers
{
    public interface IRewardManager
    {
        public event EventHandler OnRewardsProcessComplete;

        public void DestroyRewards();
        public void EnqueueReward(IRewardStats reward);
        public bool ProcessRewards();
        public void RewardExperience(double experience);
    }
}