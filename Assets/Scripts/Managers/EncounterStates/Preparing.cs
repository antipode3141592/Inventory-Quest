using FiniteStateMachine;
using InventoryQuest.Traveling;
using System;

namespace InventoryQuest.Managers.States
{
    public class Preparing : IState
    {
        IPartyController _partyController;

        public Preparing(IPartyController partyController)
        {
            _partyController = partyController;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool EndState { get; private set; } = false;

        public void OnEnter()
        {
            EndState = false;
            _partyController.IdleAll();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void Continue()
        {
            EndState = true;
        }
    }
}