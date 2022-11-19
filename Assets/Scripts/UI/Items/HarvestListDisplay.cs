using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class HarvestListDisplay: MonoBehaviour, IContainersDisplay, IOnMenuShow
    {
        IHarvestManager _harvestManager;

        readonly List<ContainerIcon> pileIcons = new();

        [SerializeField]
        ContainerIcon _lootIconPrefab;

        [Inject]
        public void Init(IHarvestManager harvestManager)
        { 
            _harvestManager = harvestManager;
        }

        void Start()
        {
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

        public void ContainerSelected(string containerGuid)
        {
            foreach (var icon in pileIcons)
            {
                if (icon.ContainerGuid == containerGuid)
                {
                    icon.IsSelected = true;
                    _harvestManager.SelectPile(containerGuid);
                }
                else
                    icon.IsSelected = false;
            }
        }

        public void SetContainers()
        {
            if (_harvestManager.Piles.Count == 0) return;
            foreach (var pile in _harvestManager.Piles.Values)
            {
                ContainerIcon icon = Instantiate<ContainerIcon>(_lootIconPrefab, transform);
                icon.SetContainerIcon(guid: pile.GuId, image: pile.Item.Stats.PrimarySprite, containersDisplay: this);
                icon.IsSelected = false;
                pileIcons.Add(icon);

            }

        }

        public void DestroyContainers()
        {
            for (int i = 0; i < pileIcons.Count; i++)
            {
                Destroy(pileIcons[i].gameObject);
            }
            pileIcons.Clear();
        }

        public void OnShow()
        {
        }
    }
}
