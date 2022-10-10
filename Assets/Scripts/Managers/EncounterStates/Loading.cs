using Data;
using FiniteStateMachine;
using InventoryQuest.Traveling;
using System;

namespace InventoryQuest.Managers.States
{
    public class Loading : IState
    {
        IGameStateDataSource _gameStateDataSource;
        IPartyController _partyController;

        public Loading(IGameStateDataSource gameStateDataSource, IPartyController partyController)
        {
            _gameStateDataSource = gameStateDataSource;
            _partyController = partyController;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public event EventHandler<string> OnEncounterLoaded;

        public bool IsLoaded { get; set; } = false;

        public bool ManageInventory { get; set; } = false;

        public void OnEnter()
        {
            IsLoaded = false;
            ManageInventory = false;
            _gameStateDataSource.SetCurrentEncounter();
            OnEncounterLoaded?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            //PixelCrushers.DialogueSystem.DialogueManager.ShowAlert(_gameStateDataSource.CurrentEncounter.Description);
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            _partyController.IdleAll();
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

      
    }
}