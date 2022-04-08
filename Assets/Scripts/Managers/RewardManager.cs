﻿using Data;
using Data.Interfaces;
using Data.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class RewardManager: MonoBehaviour
    {
        ContainerDisplayManager _displayManager;
        IRewardDataSource _rewardDataSource;
        IItemDataSource _dataSource;
        ILootTableDataSource _lootTableDataSource;
        LootTable _lootTable;

        bool isProcessing = false;

        public Dictionary<string, Container> LootPiles = new();

        public string SelectedPileId;
        
        Queue<IReward> rewardQueue = new();

        public EventHandler OnRewardsProcessStart;
        public EventHandler OnRewardsProcessComplete;

        [Inject]
        public void Init(IItemDataSource dataSource, IRewardDataSource rewardDataSource, ContainerDisplayManager displayManager, ILootTableDataSource lootTableDataSource) 
        {
            _dataSource = dataSource;
            _rewardDataSource = rewardDataSource;
            _displayManager = displayManager;
            _lootTableDataSource = lootTableDataSource;
        }

        private void Awake()
        {
            _lootTable = new LootTable(_lootTableDataSource);
        }

        #region Processing Rewards Queue

        public void EnqueueReward(string rewardId)
        {
            //get IRewardStats from IRewardDataSource
            var stats = _rewardDataSource.GetRewardById(rewardId);
            //get IReward from RewardFactory
            var reward = RewardFactory.GetReward(stats);
            if (reward is null) return;
            rewardQueue.Enqueue(reward);
        }

        public void ProcessRewards()
        {
            if (isProcessing) return;
            isProcessing = true;
            OnRewardsProcessStart?.Invoke(this, EventArgs.Empty);
            while(rewardQueue.Count > 0)
            {
                var reward = rewardQueue.Dequeue();
                ProcessReward(reward);
            }
            isProcessing = false;
            OnRewardsProcessComplete?.Invoke(this, EventArgs.Empty);
        }

        

        void ProcessReward(IReward reward)
        {
            ItemReward itemReward = reward as ItemReward;
            if (itemReward is not null)
            {
                var lootPile = (Container)ItemFactory.GetItem((ContainerStats)_dataSource.GetItemStats("loot_pile_small"));
                LootPiles.Add(lootPile.GuId, lootPile);
                TryAutoPlaceToContainer(lootPile, ItemFactory.GetItem(_dataSource.GetItemStats(itemReward.ItemId)));
                PlaceRandomLootInContainer(lootPile, "common_loot");
            }
            RandomItemReward randomItemReward = reward as RandomItemReward;
            if (randomItemReward is not null)
            {
                var lootPile = (Container)ItemFactory.GetItem((ContainerStats)_dataSource.GetItemStats(randomItemReward.LootContainerId));
                LootPiles.Add(lootPile.GuId,lootPile);
                PlaceRandomLootInContainer(lootPile, randomItemReward.LootTableId);
            }
            CharacterReward characterReward = reward as CharacterReward;
            if (characterReward is not null)
            {

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
                TryAutoPlaceToContainer(container, item);
            }
        }

        static bool TryAutoPlaceToContainer(Container container, IItem item)
        {
            for (int _r = 0; _r < container.Dimensions.row; _r++)
            {
                for (int _c = 0; _c < container.Dimensions.column; _c++)
                {
                    //try placing in default facing
                    if (container.TryPlace(item, new Coor(_r, _c)))
                        return true;
                    //try placing in each other facing
                    if (!item.Shape.IsRotationallySymmetric)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            item.Shape.Rotate(1);
                            if (container.TryPlace(item, new Coor(_r, _c)))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        public void SelectLootPile(string containerGuid)
        {
            if (LootPiles.ContainsKey(containerGuid))
            {
                SelectedPileId = containerGuid;
                _displayManager.ConnectLootContainer(LootPiles[containerGuid]);
            }
            
        }
    }
}