using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class LootListDisplay : MonoBehaviour, IOnMenuShow, IContainersDisplay
    {
        IRewardManager _rewardManager;
        IHarvestManager _harvestManager;

        readonly List<ContainerIcon> lootIcons = new();

        [SerializeField]
        ContainerIcon _lootIconPrefab;

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
            DestroyContainers();
        }

        void OnHarvestStartedHandler(object sender, EventArgs e)
        {
            SetContainers();
        }

        void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            DestroyContainers();
        }

        void OnRewardsProcessCompleteHandler(object sender, EventArgs e)
        {
            SetContainers();
        }

        public void ContainerSelected(string containerGuid)
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

        public void SetContainers()
        {
            if (_rewardManager.Piles.Count == 0) return;
            foreach (var pile in _rewardManager.Piles.Values)
            {
                ContainerIcon icon = Instantiate<ContainerIcon>(_lootIconPrefab, transform);
                icon.SetContainerIcon(guid: pile.GuId, image: pile.Item.Stats.PrimarySprite, containersDisplay: this);
                icon.IsSelected = false;
                lootIcons.Add(icon);

            }

        }

        public void DestroyContainers()
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
