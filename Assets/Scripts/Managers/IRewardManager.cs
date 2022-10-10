using Data.Items;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IRewardManager
    {
        void DestroyRewards();
        void EnqueueReward(string rewardId);
        bool ProcessRewards();
        void SelectLootPile(string containerGuid);

        public IDictionary<string, Container> LootPiles { get; }

        public event EventHandler OnRewardsProcessStart;
        public event EventHandler OnRewardsProcessComplete;
        public event EventHandler OnRewardsCleared;
        public event EventHandler<Container> OnLootPileSelected;
    }
}