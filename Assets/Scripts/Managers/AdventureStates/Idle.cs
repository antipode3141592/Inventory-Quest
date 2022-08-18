using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class Idle : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool BeginPathfinding;

        public void OnEnter()
        {
            BeginPathfinding = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
            
        }

        public void StartPath()
        {
            BeginPathfinding = true;
        }
    }
}