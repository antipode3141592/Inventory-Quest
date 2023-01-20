using Data;
using Data.Encounters;
using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class Pathfinding : IState
    {
        public bool EndState = false;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Pathfinding()
        {

        }

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