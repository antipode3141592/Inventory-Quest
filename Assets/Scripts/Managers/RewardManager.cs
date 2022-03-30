using Data;
using Data.Interfaces;
using Data.Rewards;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class RewardManager: MonoBehaviour
    {
        PartyManager _partyManager;
        ContainerDisplayManager _displayManager;
        IRewardDataSource _rewardDataSource;
        IItemDataSource _dataSource;
        ILootTableDataSource _lootTableDataSource;
        LootTable _lootTable;

        Container lootPile;
        
        Queue<IReward> rewardQueue = new Queue<IReward>();

        public EventHandler OnRewardsProcessStart;
        public EventHandler OnRewardsProcessComplete;

        [Inject]
        public void Init(IItemDataSource dataSource, IRewardDataSource rewardDataSource, PartyManager partyManager, ContainerDisplayManager displayManager, ILootTableDataSource lootTableDataSource) 
        {
            _dataSource = dataSource;
            _rewardDataSource = rewardDataSource;
            _partyManager = partyManager;
            _displayManager = displayManager;
            _lootTableDataSource = lootTableDataSource;
        }

        private void Awake()
        {
            _lootTable = new LootTable(_lootTableDataSource);
            lootPile = (Container)ItemFactory.GetItem((ContainerStats)_dataSource.GetItemStats("loot_pile_small"));
            if (rewardQueue.Count == 0) return;
            ProcessRewards();
        }

        private void Start()
        {
            _displayManager.ConnectLootContainer(lootPile);
        }


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
            OnRewardsProcessStart?.Invoke(this, EventArgs.Empty);
            while(rewardQueue.Count > 0)
            {
                var reward = rewardQueue.Dequeue();
                ProcessReward(reward);
            }
            OnRewardsProcessComplete?.Invoke(this, EventArgs.Empty);
        }

        

        void ProcessReward(IReward reward)
        {
            ItemReward itemReward = reward as ItemReward;
            if (itemReward is not null)
            {

            }
            RandomItemReward randomItemReward = reward as RandomItemReward;
            if (randomItemReward is not null)
            {
                PlaceRandomLootInContainer(randomItemReward);
            }
            CharacterReward characterReward = reward as CharacterReward;
            if (characterReward is not null)
            {

            }
        }

        public void PlaceRandomLootInContainer(RandomItemReward reward)
        {
            while (!lootPile.IsFull)
            {
                Rarity rarity = _lootTable.GetRandomRarity(reward.LootTableId);
                IItem item = ItemFactory.GetItem(_dataSource.GetRandomItemStats(rarity));
                TryAutoPlaceToContainer(lootPile, item);
            }
        }

        static bool TryAutoPlaceToContainer(Container container, IItem item)
        {
            for (int _r = 0; _r < container.ContainerSize.row; _r++)
            {
                for (int _c = 0; _c < container.ContainerSize.column; _c++)
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
    }
}
