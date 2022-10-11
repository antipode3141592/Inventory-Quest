using Data;
using Data.Items;
using Data.Rewards;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class RewardManager : MonoBehaviour, IRewardManager
    {
        IPartyManager _partyManager;
        IRewardDataSource _rewardDataSource;
        IItemDataSource _dataSource;
        ILootTableDataSource _lootTableDataSource;
        LootTable _lootTable;

        bool isProcessing = false;

        public IDictionary<string, Container> LootPiles { get; } = new Dictionary<string, Container>();

        List<IItem> deleteItems = new List<IItem>();

        public string SelectedPileId;

        Queue<IReward> rewardQueue = new();

        public event EventHandler OnRewardsProcessStart;
        public event EventHandler OnRewardsProcessComplete;
        public event EventHandler OnRewardsCleared;
        public event EventHandler<Container> OnLootPileSelected;

        [Inject]
        public void Init(IPartyManager partyManager, IItemDataSource dataSource, IRewardDataSource rewardDataSource, ILootTableDataSource lootTableDataSource)
        {
            _partyManager = partyManager;
            _dataSource = dataSource;
            _rewardDataSource = rewardDataSource;
            _lootTableDataSource = lootTableDataSource;
        }

        void Awake()
        {
            _lootTable = new LootTable(_lootTableDataSource);
            Lua.RegisterFunction("RewardExperience", this, SymbolExtensions.GetMethodInfo(() => RewardExperience(0)));
        }

        public void RewardExperience(double experience)
        {
            foreach (var character in _partyManager.CurrentParty.Characters)
            {
                character.Value.CurrentExperience += (int)experience;
            }
        }

        #region Processing Rewards Queue

        public void EnqueueReward(string rewardId)
        {
            Debug.Log($"EnqueueReward({rewardId})...");
            //get IRewardStats from IRewardDataSource
            var stats = _rewardDataSource.GetRewardById(rewardId);
            //get IReward from RewardFactory
            var reward = RewardFactory.GetReward(stats);
            if (reward is null) return;
            rewardQueue.Enqueue(reward);
        }

        public bool ProcessRewards()
        {
            if (isProcessing) return false;
            isProcessing = true;
            int rewardsProcessed = rewardQueue.Count;
            OnRewardsProcessStart?.Invoke(this, EventArgs.Empty);
            while (rewardQueue.Count > 0)
            {
                var reward = rewardQueue.Dequeue();
                ProcessReward(reward);
            }
            isProcessing = false;
            OnRewardsProcessComplete?.Invoke(this, EventArgs.Empty);
            return rewardsProcessed > 0;
        }



        void ProcessReward(IReward reward)
        {
            Debug.Log($"Processing Reward {reward.Name}", this);
            ItemReward itemReward = reward as ItemReward;
            if (itemReward is not null)
            {
                var lootPile = (Container)ItemFactory.GetItem((ContainerStats)_dataSource.GetItemStats("loot_pile_small"));
                LootPiles.Add(lootPile.GuId, lootPile);
                ItemPlacementHelpers.TryAutoPlaceToContainer(lootPile, ItemFactory.GetItem(_dataSource.GetItemStats(itemReward.ItemId)));
                PlaceRandomLootInContainer(lootPile, "common_loot");
            }
            RandomItemReward randomItemReward = reward as RandomItemReward;
            if (randomItemReward is not null)
            {
                var lootPile = (Container)ItemFactory.GetItem((ContainerStats)_dataSource.GetItemStats(randomItemReward.LootContainerId));
                LootPiles.Add(lootPile.GuId, lootPile);
                PlaceRandomLootInContainer(lootPile, randomItemReward.LootTableId);
            }
            CharacterReward characterReward = reward as CharacterReward;
            if (characterReward is not null)
            {

            }
        }

        public void DestroyRewards()
        {
            Debug.Log($"Destroying reward piles and items", this);
            deleteItems.Clear();
            foreach (var container in LootPiles.Values)
            {
                foreach (var content in container.Contents.Values)
                {
                    deleteItems.Add(content.Item);
                }
                deleteItems.Add(container);
            }

            for (int i = 0; i < deleteItems.Count; i++)
            {
                deleteItems[i] = null;
            }
            LootPiles.Clear();
            OnRewardsCleared?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region HarvestFunctions
        public void AddLoot(string containerId, string itemId, int quantity)
        {
            var lootPile = (Container)ItemFactory.GetItem((ContainerStats)_dataSource.GetItemStats(containerId));
            LootPiles.Add(lootPile.GuId, lootPile);
            for (int i = 0; i < quantity; i++)
            {
                ItemPlacementHelpers.TryAutoPlaceToContainer(lootPile, ItemFactory.GetItem(_dataSource.GetItemStats(itemId)));
            }
        }

        #endregion

        #region Item Placement Functions
        void PlaceRandomLootInContainer(Container container, string lootTableId)
        {
            while (!container.IsFull)
            {
                Rarity rarity = _lootTable.GetRandomRarity(lootTableId);
                IItem item = ItemFactory.GetItem(_dataSource.GetRandomItemStats(rarity));
                ItemPlacementHelpers.TryAutoPlaceToContainer(container, item);
            }
        }
        #endregion

        public void SelectLootPile(string containerGuid)
        {
            if (LootPiles.ContainsKey(containerGuid))
            {
                SelectedPileId = containerGuid;
                OnLootPileSelected?.Invoke(this, LootPiles[containerGuid]);
            }

        }
    }
}
