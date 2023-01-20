using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class HoldingItem: IState
    {
        IInputManager _inputManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public HoldingItem(IInputManager inputManager)
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
            _inputManager.CheckRotateAction();
            _inputManager.CheckSubmitAction();
        }
    }
}
