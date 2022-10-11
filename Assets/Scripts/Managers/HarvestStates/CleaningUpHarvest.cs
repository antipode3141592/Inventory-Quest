using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class CleaningUpHarvest : IState
    {
        IRewardManager _rewardManager;

        public bool IsDone;
        public CleaningUpHarvest(IRewardManager rewardManager)
        {
            _rewardManager = rewardManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            IsDone = true;
            _rewardManager.DestroyRewards();
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