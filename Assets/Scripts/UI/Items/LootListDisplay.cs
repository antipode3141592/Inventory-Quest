using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class LootListDisplay : MonoBehaviour, IOnMenuShow, IItemPileDisplay
    {
        IRewardManager _rewardManager;
        IHarvestManager _harvestManager;

        readonly List<LootIcon> lootIcons = new();

        [SerializeField]
        LootIcon _lootIconPrefab;

        [Inject]
        public void Init(IRewardManager rewardManager, IHarvestManager harvestManager)
        {
            _rewardManager = rewardManager;
            _harvestManager = harvestManager;
        }

        void Start()
        {
            _rewardManager.OnRewardsProcessComplete += OnRewardsProcessCompleteHandler;
            _rewardManager.OnRewardsCleared += OnRewardsClearedHandler;

            _harvestManager.Harvesting.StateEntered += OnHarvestStartedHandler;
            _harvestManager.CleaningUpHarvest.StateEntered += OnHarvestCleaningUpStartedHandler;
        }

        void OnHarvestCleaningUpStartedHandler(object sender, EventArgs e)
        {
            DestroyPiles();
        }

        void OnHarvestStartedHandler(object sender, EventArgs e)
        {
            SetPiles();
        }

        void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            DestroyPiles();
        }

        void OnRewardsProcessCompleteHandler(object sender, EventArgs e)
        {
            SetPiles();
        }

        public void PileSelected(string containerGuid)
        {
            foreach (var icon in lootIcons)
            {
                if (icon.ContainerGuid == containerGuid)
                {
                    icon.IsSelected = true;
                    _rewardManager.SelectPile(containerGuid);
                }
                else
                    icon.IsSelected = false;
            }
        }

        public void SetPiles()
        {
            if (_rewardManager.Piles.Count == 0) return;
            foreach (var pile in _rewardManager.Piles.Values)
            {
                LootIcon icon = Instantiate<LootIcon>(_lootIconPrefab, transform);
                icon.PileDisplay = this;
                icon.SetupLootIcon(guid: pile.GuId, imagePath: pile.Stats.SpritePath);
                icon.IsSelected = false;
                lootIcons.Add(icon);

            }

        }

        public void DestroyPiles()
        {
            for (int i = 0; i < lootIcons.Count; i++)
            {
                Destroy(lootIcons[i].gameObject);
            }
            lootIcons.Clear();
        }

        public void OnShow()
        {
            
        }
    }
}
