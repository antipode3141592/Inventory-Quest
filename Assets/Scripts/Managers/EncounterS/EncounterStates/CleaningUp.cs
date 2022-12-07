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
        public event EventHandler RequestShowInventory;

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
                RequestShowInventory?.Invoke(this, EventArgs.Empty);
               
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
            for(int i = 0; i < _encounterManager.EncounterModifiers.Count; i++)
            {
                var modifier = _encounterManager.EncounterModifiers[i];
                modifier.Character.RemoveModifiers(_encounterManager.EncounterModifiers[i].Modifiers);
            }
        }
    }
}