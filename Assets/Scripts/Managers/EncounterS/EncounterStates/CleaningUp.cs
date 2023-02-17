using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class CleaningUp : IState
    {
        readonly IRewardManager _rewardManager;
        readonly IGameStateDataSource _gameStateDataSource;
        readonly IInputManager _inputManager;
        readonly IEncounterManager _encounterManager;

        public CleaningUp(IRewardManager rewardManager, IGameStateDataSource gameStateDataSource, IInputManager inputManager, IEncounterManager encounterManager)
        {
            _rewardManager = rewardManager;
            _gameStateDataSource = gameStateDataSource;
            _inputManager = inputManager;
            _encounterManager = encounterManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool EndState { get; private set; } = false;

        public bool EncounterAvailable { get; private set; } = false;

        public void OnEnter()
        {
            EndState = false;
            EncounterAvailable = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
            _inputManager.CloseInventoryCommand += CloseInventoryHandler;
            if (_rewardManager.ProcessRewards())
            {
                _inputManager.OpenInventory();
            } 
            else
            {
                Continue();
            }
        }

        public void OnExit()
        {
            _inputManager.CloseInventoryCommand -= CloseInventoryHandler;
            EndEncounterEffects();
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void Continue()
        {
            if (EndState) return;
            EndState = true;

            _rewardManager.DestroyRewards();
            //check for end of path
            _gameStateDataSource.CurrentIndex++;
            if (_gameStateDataSource.CurrentIndex < _gameStateDataSource.CurrentPathStats.EncounterStats.Count)
                EncounterAvailable = true;
        }

        void CloseInventoryHandler(object sender, EventArgs e)
        {
            Continue();
        }

        void EndEncounterEffects()
        {
            while (_encounterManager.EncounterModifiers.Count > 0)
            {
                var modifier = _encounterManager.EncounterModifiers.Dequeue();
                modifier.Character.RemoveModifiers(modifier.StatModifiers);
                modifier.Character.RemoveModifiers(modifier.ResistanceModifiers);
                if (modifier.EncounterLengthEffect is null)
                    return;
                modifier.EncounterLengthEffect.ResetUsage();
            }
        }
    }
}