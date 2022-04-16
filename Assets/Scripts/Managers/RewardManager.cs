using Data;
using Data.Items;
using Data.Rewards;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class RewardManager : MonoBehaviour, IRewardManager
    {
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
        public void Init(IItemDataSource dataSource, IRewardDataSource rewardDataSource, ILootTableDataSource lootTableDataSource)
        {
            _dataSource = dataSource;
            _rewardDataSource = rewardDataSource;
            _lootTableDataSource = lootTableDataSource;
        }

        private void Awake()
        {
            _lootTable = new LootTable(_lootTableDataSource);
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

        public void ProcessRewards()
        {
            if (isProcessing) return;
            isProcessing = true;
            OnRewardsProcessStart?.Invoke(this, EventArgs.Empty);
            while (rewardQueue.Count > 0)
            {
                var reward = rewardQueue.Dequeue();
                ProcessReward(reward);
            }
            isProcessing = false;
            OnRewardsProcessComplete?.Invoke(this, EventArgs.Empty);
        }



        void ProcessReward(IReward reward)
        {
            Debug.Log($"Processing Reward {reward.Name}", this);
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
                OnLootPileSelected?.Invoke(this, LootPiles[containerGuid]);
                //_displayManager.ConnectLootContainer(LootPiles[containerGuid]);
            }

        }
    }
}
