﻿using Data;
using FiniteStateMachine;
using System;

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

        public bool IsLoaded { get; private set; } = false;

        public void OnEnter()
        {
            IsLoaded = false;
            _gameStateDataSource.SetCurrentEncounter();
            OnEncounterLoaded?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
            IsLoaded = true;
        }

      
    }
}