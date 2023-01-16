using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class AdventureManagerStart: IState
    {
        IAdventureManager _adventureManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public AdventureManagerStart(IAdventureManager adventureManager)
        {
            _adventureManager = adventureManager;
        }

        public void OnEnter()
        {
            _adventureManager.GameEnding = false;
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