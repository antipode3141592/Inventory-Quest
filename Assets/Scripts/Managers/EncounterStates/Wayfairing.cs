using Data;
using FiniteStateMachine;
using InventoryQuest.Traveling;
using System;

namespace InventoryQuest.Managers.States
{
    public class Wayfairing : IState
    {
        TravelSettings _travelSettings;
        IPartyController _partyController;
        IDeltaTimeTracker _deltaTimeTracker;

        public bool IsDone = false;

        bool enableTimer = false;
        float timer = 0f;

        public event EventHandler<float> TimerPercentComplete;

        public Wayfairing(IPartyController partyController, IDeltaTimeTracker deltaTimeTracker, TravelSettings travelSettings)
        {
            _partyController = partyController;
            _deltaTimeTracker = deltaTimeTracker;
            _travelSettings = travelSettings;
        }

        //party autoscrolls for one unit

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            IsDone = false;
            enableTimer = true;
            timer = 0f;
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
            if (!enableTimer) return;
            timer += _deltaTimeTracker.DeltaTime;
            TimerPercentComplete?.Invoke(this, timer / _travelSettings.WayfairingEncounterTime);
            if (timer < _travelSettings.WayfairingEncounterTime) return;
            IsDone = true;
        }
    }
}