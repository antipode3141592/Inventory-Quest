using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class Pathfinding : IState
    {
        IGameStateDataSource _gameStateDataSource;

        bool endState = false;
        bool returnState = false;

        public bool EndState => endState;
        public bool ReturnState => returnState;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Pathfinding(IGameStateDataSource gameStateDataSource)
        {
            _gameStateDataSource = gameStateDataSource;
        }

        public void OnEnter()
        {
            endState = false;
            returnState = false;
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
            endState = true;
        }

        public void Return()
        {
            _gameStateDataSource.SetDestinationLocation(_gameStateDataSource.CurrentLocation.Id);
            returnState = true;
        }
    }
}