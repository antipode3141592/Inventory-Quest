using InventoryQuest.Managers.States;

namespace InventoryQuest.Managers
{
    public interface IHarvestManager
    {
        public HarvestTypes CurrentHarvestType { get; }

        public Idle Idle { get; }
        public LoadingHarvest LoadingHarvest { get; }
        public Harvesting Harvesting { get; }
        public ResolvingHarvest ResolvingHarvest { get; }
        public CleaningUpHarvest CleaningUpHarvest { get; }

        public void BeginHarvest(HarvestTypes harvestType);
    }
}
