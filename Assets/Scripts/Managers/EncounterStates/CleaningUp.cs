using Data;
using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class CleaningUp : IState
    {
        IRewardManager _rewardManager;
        IGameStateDataSource _gameStateDataSource;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool EndState { get; private set; } = false;

        public bool EncounterAvailable { get; private set; } = false;

        public void OnEnter()
        {
            EndState = false;
            EncounterAvailable = false;
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
            if (EndState) return;
            EndState = true;
            //destroy remaining rewards
            _rewardManager.DestroyRewards();
            //destroy all dropped items

            //check for end of path
            _gameStateDataSource.CurrentIndex++;
            if (_gameStateDataSource.CurrentIndex < _gameStateDataSource.CurrentPath.Length)
                EncounterAvailable = true;
        }
    }
}