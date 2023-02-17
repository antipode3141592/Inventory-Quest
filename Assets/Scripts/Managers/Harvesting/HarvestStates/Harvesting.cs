using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.Managers.States
{
    public class Harvesting : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool IsDone;

        public void OnEnter()
        {
            IsDone = false;
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
            IsDone = true;
        }
    }
}