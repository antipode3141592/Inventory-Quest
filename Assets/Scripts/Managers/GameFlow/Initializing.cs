using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class Initializing : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool ManagersLoaded = false;

        public void OnEnter()
        {
            ManagersLoaded = false;
            BeginLoading();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
        }

        void BeginLoading()
        {

        }
    }
}
