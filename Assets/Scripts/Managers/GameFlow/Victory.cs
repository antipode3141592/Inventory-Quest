using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class Victory : IState
    {
        IGameManager _gameManager;

        public Victory(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            _gameManager.GameWin();
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
