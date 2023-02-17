using FiniteStateMachine;
using InventoryQuest.Audio;
using System;

namespace InventoryQuest.Managers
{
    public class InLocation: IState
    {
        readonly IGameStateDataSource _gameStateDataSource;
        readonly IAudioManager _audioManager;

        public bool EndState = false;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public InLocation(IGameStateDataSource gameStateDataSource, IAudioManager audioManager)
        {
            _gameStateDataSource = gameStateDataSource;
            _audioManager = audioManager;
        }

        public void OnEnter()
        {
            EndState = false;
            _gameStateDataSource.SetCurrentLocation(_gameStateDataSource.DestinationLocation.Id);
            _gameStateDataSource.SetDestinationLocation("");
            _audioManager.PlayMusicTrack(_gameStateDataSource.CurrentLocation.LocationMusic);
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