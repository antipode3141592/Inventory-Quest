using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class AdventureManagerStart: IState
    {
        readonly IGameManager _gameManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public AdventureManagerStart(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnEnter()
        {
            _gameManager.IsGameBegining = false;
            _gameManager.IsGameOver = false;
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