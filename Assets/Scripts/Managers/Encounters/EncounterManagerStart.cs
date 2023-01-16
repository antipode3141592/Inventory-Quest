using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class EncounterManagerStart: IState
    {
        IEncounterManager _encounterManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public EncounterManagerStart(IEncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        public void OnEnter()
        {
            _encounterManager.GameBeginning = false;
            _encounterManager.GameEnding = false;
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
