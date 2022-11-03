using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class HarvestListDisplay: MonoBehaviour, IItemPileDisplay, IOnMenuShow
    {
        IHarvestManager _harvestManager;

        readonly List<LootIcon> pileIcons = new();

        [SerializeField]
        LootIcon _lootIconPrefab;

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
            DestroyPiles();
        }

        void OnHarvestStartedHandler(object sender, EventArgs e)
        {
            SetPiles();
        }

        public void PileSelected(string containerGuid)
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

        public void SetPiles()
        {
            if (_harvestManager.Piles.Count == 0) return;
            foreach (var pile in _harvestManager.Piles.Values)
            {
                LootIcon icon = Instantiate<LootIcon>(_lootIconPrefab, transform);
                icon.PileDisplay = this;
                icon.SetupLootIcon(guid: pile.GuId, imagePath: pile.Item.Stats.SpritePath);
                icon.IsSelected = false;
                pileIcons.Add(icon);

            }

        }

        public void DestroyPiles()
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
