using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class EncounterIdle : IState
    {
        IEncounterManager _encounterManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool EndState;

        public EncounterIdle(IEncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        public void OnEnter()
        {
            EndState = false;
            _encounterManager.GameBeginning = false;
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