using FiniteStateMachine;
using InventoryQuest.Traveling;
using System;
using UnityEngine;

namespace InventoryQuest.Managers.States
{
    public class Wayfairing : IState
    {
        IPartyController _partyController;

        public Wayfairing(IPartyController partyController)
        {
            _partyController = partyController;
        }

        public float TotalDistanceMoved => _partyController.DistanceMoved;

        //party autoscrolls for one unit

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            Debug.Log($"Entering Wayfairing State...");
            _partyController.MoveAll();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            //check for encounter
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
            //move party to the right
            // store total distance
            
        }
    }
}