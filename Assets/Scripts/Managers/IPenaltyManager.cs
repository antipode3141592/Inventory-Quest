using Data.Penalties;
using System;

namespace InventoryQuest.Managers
{
    public interface IPenaltyManager
    {
        public event EventHandler OnPenaltyProcessStart;
        public event EventHandler OnPenaltyProcessComplete;

        public void EnqueuePenalty(IPenaltyStats penaltyStats);
        public bool ProcessPenalties();
    }
}
