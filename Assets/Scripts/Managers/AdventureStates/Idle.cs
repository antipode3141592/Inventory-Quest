using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class Idle : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool EndIdle;

        public void OnEnter()
        {
            EndIdle = false;
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
            EndIdle = true;
        }
    }
}