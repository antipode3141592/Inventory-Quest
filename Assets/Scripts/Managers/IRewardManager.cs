using Data.Items;
using Data.Rewards;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IRewardManager
    {
        public IDictionary<string, IContainer> Piles { get; }

        public event EventHandler OnRewardsProcessStart;
        public event EventHandler OnRewardsProcessComplete;
        public event EventHandler OnRewardsCleared;
        public event EventHandler<IContainer> OnPileSelected;

        public void DestroyRewards();
        public void EnqueueReward(IRewardStats reward);
        public bool ProcessRewards();
        public void SelectPile(string containerGuid);
        public void RewardExperience(double experience);
    }
}