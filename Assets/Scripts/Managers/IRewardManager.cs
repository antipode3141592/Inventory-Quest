using Data.Items;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IRewardManager
    {
        public IDictionary<string, Container> LootPiles { get; }

        public event EventHandler OnRewardsProcessStart;
        public event EventHandler OnRewardsProcessComplete;
        public event EventHandler OnRewardsCleared;
        public event EventHandler<Container> OnLootPileSelected;

        public void DestroyRewards();
        public void EnqueueReward(string rewardId);
        public bool ProcessRewards();
        public void SelectLootPile(string containerGuid);
        
        public void AddLoot(string containerId, string itemId, int quantity);
    }
}