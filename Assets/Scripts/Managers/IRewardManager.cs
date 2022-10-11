using Data.Items;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IRewardManager
    {
        public IDictionary<string, Container> Piles { get; }

        public event EventHandler OnRewardsProcessStart;
        public event EventHandler OnRewardsProcessComplete;
        public event EventHandler OnRewardsCleared;
        public event EventHandler<Container> OnPileSelected;

        public void DestroyRewards();
        public void EnqueueReward(string rewardId);
        public bool ProcessRewards();
        public void SelectPile(string containerGuid);
    }
}