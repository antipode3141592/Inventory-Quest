using Data.Items;
using InventoryQuest.Managers.States;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IHarvestManager
    {
        public IDictionary<string, IContainer> Piles { get; }

        public HarvestTypes CurrentHarvestType { get; }

        public Idle Idle { get; }
        public LoadingHarvest LoadingHarvest { get; }
        public Harvesting Harvesting { get; }
        public ResolvingHarvest ResolvingHarvest { get; }
        public CleaningUpHarvest CleaningUpHarvest { get; }

        public void BeginHarvest(HarvestTypes harvestType);

        public void PopulateHarvest(string containerId, string itemId, int quantity);

        public void DestroyHarvest();

        public void SelectPile(string containerGuid);

        public event EventHandler<IContainer> OnPileSelected;
        public event EventHandler OnItemCut;
    }
}
