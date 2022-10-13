using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class ResolvingHarvest: IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool IsDone;

        public void OnEnter()
        {
            IsDone = true;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
        }

        public void Resolve()
        {
            //do some calculations on harvested stuff
            IsDone = true;
        }
    }
}
