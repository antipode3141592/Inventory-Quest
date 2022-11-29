using Data;
using Data.Encounters;
using FiniteStateMachine;
using System;
using UnityEngine;

namespace InventoryQuest.Managers.States
{
    public class Loading : IState
    {
        IGameStateDataSource _gameStateDataSource;

        public Loading(IGameStateDataSource gameStateDataSource)
        {
            _gameStateDataSource = gameStateDataSource;
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
            _gameStateDataSource.OnCurrentEncounterSet += OnEncounterSetHandler;
            _gameStateDataSource.SetCurrentEncounter();
            StateEntered?.Invoke(this, EventArgs.Empty);   
        }

        void OnEncounterSetHandler(object sender, string e)
        {
            Debug.Log($"OnEncounterSetHandler:  current encounter: {_gameStateDataSource.CurrentEncounter.Id}");
            _gameStateDataSource.OnCurrentEncounterSet -= OnEncounterSetHandler;
            IEmptyEncounterStats emptyEncounter = _gameStateDataSource.CurrentEncounter.Stats as IEmptyEncounterStats;
            IRestEncounterStats restEncounter = _gameStateDataSource.CurrentEncounter.Stats as IRestEncounterStats;
            //rest encounters auto-resolve
            if (restEncounter is not null || emptyEncounter is not null)
            {
                Debug.Log($"rest or empty encounter found.  Loading being set to IsLoaded");
                IsLoaded = true;
            }
            OnEncounterLoaded?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
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