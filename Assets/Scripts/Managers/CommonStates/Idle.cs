using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class Idle : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool EndState;

        public void OnEnter()
        {
            EndState = false;
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