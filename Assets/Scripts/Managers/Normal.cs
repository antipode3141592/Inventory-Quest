using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class Normal : IState 
    {
        IInputManager _inputManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Normal(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

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
