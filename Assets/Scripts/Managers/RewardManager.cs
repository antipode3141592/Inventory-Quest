using Data;
using Data.Interfaces;
using Data.Rewards;
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

        Container lootPile;
        
        Queue<IReward> rewardQueue = new Queue<IReward>();

        public bool IsProcessing = true;

        [Inject]
        public void Init(IItemDataSource dataSource, IRewardDataSource rewardDataSource, PartyManager partyManager, ContainerDisplayManager displayManager) 
        {
            _dataSource = dataSource;
            _rewardDataSource = rewardDataSource;
            _partyManager = partyManager;
            _displayManager = displayManager;
        }

        private void Awake()
        {
            lootPile = (Container)ItemFactory.GetItem((ContainerStats)_dataSource.GetItemStats("loot_pile"));
            _displayManager.ConnectLootContainer(lootPile);
            if (rewardQueue.Count == 0) return;
            ProcessRewards();
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
            IsProcessing = true;
            while(rewardQueue.Count > 0)
            {
                var reward = rewardQueue.Dequeue();
                ProcessReward(reward);
            }
            IsProcessing = false;
        }

        

        void ProcessReward(IReward reward)
        {
            ItemReward itemReward = reward as ItemReward;
            if (itemReward is not null)
            {

            }
        }

        public void PlaceRandomLootInContainer(ILootTable lootTable, int totalItems)
        {
            
            for (int i = 0; i < totalItems; i++)
            {
                Rarity rarity = lootTable.RandomRarity;

                IItem item = ItemFactory.GetItem(_dataSource.GetRandomItemStats(rarity));
                while(!TryAutoPlaceToContainer(lootPile, item))
                {
                    if (lootPile.IsFull) break;
                    item = ItemFactory.GetItem(_dataSource.GetRandomItemStats(rarity));
                }
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
                    for (int i = 0; i < 3; i++)
                    {
                        item.Shape.Rotate(1);
                        if (container.TryPlace(item, new Coor(_r, _c)))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
