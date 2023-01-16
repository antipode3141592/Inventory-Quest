using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class InLocation: IState
    {
        IAdventureManager _adventureManager;
        IGameStateDataSource _gameStateDataSource;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public InLocation(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
        }

        public void OnEnter()
        {
            _gameStateDataSource.SetCurrentLocation(_gameStateDataSource.DestinationLocation.Stats.Id);
            _gameStateDataSource.SetDestinationLocation("");
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