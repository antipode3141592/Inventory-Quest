using FiniteStateMachine;
using InventoryQuest.Traveling;
using System;
using UnityEngine;

namespace InventoryQuest.Managers.States
{
    public class ManagingInventory : IState
    {
        IPartyController _partyController;

        public ManagingInventory(IPartyController partyController)
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
            EndState = false;
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void Continue()
        {
            Debug.Log($"ManagingInventory received Continue()");
            EndState = true;
        }
    }
}