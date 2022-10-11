using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class LootListDisplay : MonoBehaviour, IOnMenuShow
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

        private void Awake()
        {
            _rewardManager.OnRewardsProcessComplete += OnRewardsProcessCompleteHandler;
            _rewardManager.OnRewardsCleared += OnRewardsClearedHandler;

            _harvestManager.Harvesting.StateEntered += OnHarvestStartedHandler;
            _harvestManager.CleaningUpHarvest.StateEntered += OnHarvestCleaningUpStartedHandler;
        }

        private void OnHarvestCleaningUpStartedHandler(object sender, EventArgs e)
        {
            DestroyLootPiles();
        }

        private void OnHarvestStartedHandler(object sender, EventArgs e)
        {
            SetLootPiles();
        }

        private void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            DestroyLootPiles();
        }

        private void OnRewardsProcessCompleteHandler(object sender, EventArgs e)
        {
            SetLootPiles();
        }

        public void LootPileSelected(string containerGuid)
        {
            foreach (var icon in lootIcons)
            {
                if (icon.ContainerGuid == containerGuid)
                {
                    icon.IsSelected = true;
                    _rewardManager.SelectLootPile(containerGuid);
                }
                else
                    icon.IsSelected = false;
            }
        }

        public void SetLootPiles()
        {
            if (_rewardManager.LootPiles.Count == 0) return;
            foreach(var pile in _rewardManager.LootPiles.Values)
            {
                LootIcon icon = Instantiate<LootIcon>(_lootIconPrefab, transform);
                icon.LootListDisplay = this;
                icon.SetupLootIcon(guid: pile.GuId, imagePath: pile.Stats.SpritePath);
                icon.IsSelected = false;
                lootIcons.Add(icon);
                
            }

        }

        public void DestroyLootPiles()
        {
            for (int i = 0; i < lootIcons.Count; i++)
            {
                Destroy(lootIcons[i].gameObject);
            }
            lootIcons.Clear();
        }

        public void OnShow()
        {
            throw new NotImplementedException();
        }
    }
}
