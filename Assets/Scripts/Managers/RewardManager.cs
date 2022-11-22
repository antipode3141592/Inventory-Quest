﻿using Data;
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
        IItemDataSource _dataSource;
        ILootTableDataSource _lootTableDataSource;
        LootTable _lootTable;

        bool isProcessing = false;

        public IDictionary<string, IContainer> Piles { get; } = new Dictionary<string, IContainer>();

        List<IItem> deleteItems = new List<IItem>();

        public string SelectedPileId;

        Queue<IRewardStats> rewardQueue = new();

        public event EventHandler OnRewardsProcessStart;
        public event EventHandler OnRewardsProcessComplete;
        public event EventHandler OnRewardsCleared;
        public event EventHandler<IContainer> OnPileSelected;

        [Inject]
        public void Init(IPartyManager partyManager, IItemDataSource dataSource,  ILootTableDataSource lootTableDataSource)
        {
            _partyManager = partyManager;
            _dataSource = dataSource;
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

        public void EnqueueReward(IRewardStats rewardStats)
        {
            rewardQueue.Enqueue(rewardStats);
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



        void ProcessReward(IRewardStats rewardStats)
        {
            ItemRewardStats itemReward = rewardStats as ItemRewardStats;
            if (itemReward is not null)
            {
                var lootPile = ItemFactory.GetItem(_dataSource.GetById("loot_pile_small"));
                var lootContainer = lootPile.Components[typeof(IContainer)] as IContainer;
                if (lootContainer is null) return;
                Piles.Add(lootContainer.GuId, lootContainer);
                ItemPlacementHelpers.TryAutoPlaceToContainer(lootContainer, ItemFactory.GetItem(_dataSource.GetById(itemReward.ItemId)));
                PlaceRandomLootInContainer(lootContainer, "common_loot");
            }
            RandomItemRewardStats randomItemReward = rewardStats as RandomItemRewardStats;
            if (randomItemReward is not null)
            {
                var lootPile = ItemFactory.GetItem(_dataSource.GetById(randomItemReward.LootContainerId));
                var lootContainer = lootPile.Components[typeof(IContainer)] as IContainer;
                if (lootContainer is null) return;
                Piles.Add(lootPile.GuId, lootContainer);
                PlaceRandomLootInContainer(lootContainer, randomItemReward.LootTableId);
            }
            CharacterRewardStats characterReward = rewardStats as CharacterRewardStats;
            if (characterReward is not null)
            {
                _partyManager.AddCharacterToParty(characterReward.CharacterId);
            }
        }

        public void DestroyRewards()
        {
            Debug.Log($"Destroying reward piles and items", this);
            deleteItems.Clear();
            foreach (var container in Piles.Values)
            {
                foreach (var content in container.Contents.Values)
                {
                    deleteItems.Add(content.Item);
                }
                deleteItems.Add(container.Item);
            }

            for (int i = 0; i < deleteItems.Count; i++)
            {
                deleteItems[i] = null;
            }
            Piles.Clear();
            OnRewardsCleared?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Item Placement Functions
        void PlaceRandomLootInContainer(IContainer container, string lootTableId)
        {
            while (!container.IsFull)
            {
                Rarity rarity = _lootTable.GetRandomRarity(lootTableId);
                IItem item = ItemFactory.GetItem(_dataSource.GetItemByRarity(rarity));
                ItemPlacementHelpers.TryAutoPlaceToContainer(container, item);
            }
        }
        #endregion

        public void SelectPile(string containerGuid)
        {
            if (Piles.ContainsKey(containerGuid))
            {
                SelectedPileId = containerGuid;
                OnPileSelected?.Invoke(this, Piles[containerGuid]);
            }

        }
    }
}
