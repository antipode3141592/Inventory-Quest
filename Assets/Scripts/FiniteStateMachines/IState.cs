using System;

namespace FiniteStateMachine
{
    public interface IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public void Tick();
        public void OnEnter();
        public void OnExit();
    }
}