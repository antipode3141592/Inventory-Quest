using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class CleaningUpHarvest : IState
    {
        IHarvestManager _harvestManager;

        public bool IsDone;
        public CleaningUpHarvest(IHarvestManager harvestManager)
        {
            _harvestManager = harvestManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            IsDone = true;
            _harvestManager.DestroyHarvest();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }
    }
}