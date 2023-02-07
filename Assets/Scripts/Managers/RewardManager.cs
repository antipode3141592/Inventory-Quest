using Data;
using Data.Items;
using Data.Rewards;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace InventoryQuest.Managers
{
    public class RewardManager : MonoBehaviour, IRewardManager
    {
        [SerializeField] List<Tag> tagsExcludedFromLoot;

        IPartyManager _partyManager;
        IGameManager _gameManager;
        IItemDataSource _dataSource;
        ILootTableDataSource _lootTableDataSource;
        IContainerManager _containerManager;
        LootTable _lootTable;

        bool isProcessing = false;

        public string SelectedPileId;

        readonly Queue<IRewardStats> rewardQueue = new();

        public event EventHandler OnRewardsProcessComplete;

        [Inject]
        public void Init(IPartyManager partyManager, IItemDataSource dataSource,  ILootTableDataSource lootTableDataSource, IGameManager gameManager, IContainerManager containerManager)
        {
            _partyManager = partyManager;
            _dataSource = dataSource;
            _lootTableDataSource = lootTableDataSource;
            _gameManager = gameManager;
            _containerManager = containerManager;
        }

        void Start()
        {
            _lootTable = new LootTable(_lootTableDataSource);
            Lua.RegisterFunction("RewardExperience", this, SymbolExtensions.GetMethodInfo(() => RewardExperience(0)));

            _gameManager.OnGameBeginning += OnGameBeginingHandler;
        }

        void OnGameBeginingHandler(object sender, EventArgs e)
        {
            DestroyRewards();
        }

        public void RewardExperience(double experience)
        {
            if (experience <= double.Epsilon)
                return;
            QuestLog.Log($"Party members gain {(int)experience} experience.");
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
                var container = _containerManager.AddNewContainer("loot_pile_small");
                if (container is null) return;
                ItemPlacementHelpers.TryAutoPlaceToContainer(container, ItemFactory.GetItem(_dataSource.GetById(itemReward.ItemId)));
                PlaceRandomLootInContainer(container, "common_loot");
            }
            RandomItemRewardStats randomItemReward = rewardStats as RandomItemRewardStats;
            if (randomItemReward is not null)
            {
                var container = _containerManager.AddNewContainer(randomItemReward.LootContainerId);
                if (container is null) return;
                PlaceRandomLootInContainer(container, randomItemReward.LootTableId);
            }
            CharacterRewardStats characterReward = rewardStats as CharacterRewardStats;
            if (characterReward is not null)
            {
                _partyManager.AddCharacterToPartyById(characterReward.CharacterId);
            }
        }

        public void DestroyRewards()
        {
            _containerManager.DestroyContainers();
        }

        #endregion

        #region Item Placement Functions
        void PlaceRandomLootInContainer(IContainer container, string lootTableId)
        {
            while (!container.IsFull)
            {
                Rarity rarity = _lootTable.GetRandomRarity(lootTableId);
                IItem item = ItemFactory.GetItem(_dataSource.GetItemByRarity(rarity));
                if (item.Stats.IsStackable)
                    item.Quantity = Random.Range(1, item.Stats.MaxQuantity);
                ItemPlacementHelpers.TryAutoPlaceToContainer(container, item);
                if (Random.Range(0f, 1f) > 0.75f)
                    break;
            }
        }
        #endregion
    }
}
