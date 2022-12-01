using FiniteStateMachine;
using InventoryQuest.Traveling;
using System;

namespace InventoryQuest.Managers.States
{
    public class ManagingInventory : IState
    {
        readonly IPartyController _partyController;
        readonly IInputManager _inputManager;

        public ManagingInventory(IPartyController partyController, IInputManager inputManager)
        {
            _partyController = partyController;
            _inputManager = inputManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool EndState { get; private set; } = false;

        public void OnEnter()
        {
            EndState = false;
            _partyController.IdleAll();
            _inputManager.CloseInventoryCommand += CloseInventoryHandler;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }    

        public void OnExit()
        {
            _inputManager.CloseInventoryCommand -= CloseInventoryHandler;
            EndState = false;
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        void CloseInventoryHandler(object sender, EventArgs e)
        {
            EndState = true;
        }
    }
}