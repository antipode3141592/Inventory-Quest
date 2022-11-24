using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class LoadingHarvest : IState
    {
        IHarvestManager _harvestManager;

        public bool IsDone;

        public LoadingHarvest(IHarvestManager harvestManager)
        {
            _harvestManager = harvestManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            IsDone = false;
            LoadHarvest(_harvestManager.CurrentHarvestType);
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
        }

        void LoadHarvest(HarvestTypes harvestType)
        {
            switch (harvestType)
            {
                case HarvestTypes.Forest:
                    _harvestManager.PopulateHarvest("logging_pile", "log_standard", 10);
                    break;
                case HarvestTypes.Mine:
                    break;
                case HarvestTypes.Herbs:
                    break;
                default:
                    break;
            }
            IsDone = true;
        }
    }
}