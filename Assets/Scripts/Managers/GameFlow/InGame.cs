using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class InGame : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
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
