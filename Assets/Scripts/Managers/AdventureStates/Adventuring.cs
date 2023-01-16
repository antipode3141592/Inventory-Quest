using FiniteStateMachine;
using System;
using UnityEngine;

namespace InventoryQuest.Managers.States
{
    public class Adventuring : IState
    {
        readonly IEncounterManager _encounterManager;
        readonly IGameStateDataSource _gameStateDataSource;
        
        public bool EndAdventure = false;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Adventuring(IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource)
        {
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
        }

        public void OnEnter()
        {
            EndAdventure = false;
            StartAdventure();

            _encounterManager.Idle.StateEntered += EncounterIdle;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        void EncounterIdle(object sender, EventArgs e)
        {
            EndAdventure = true;
        }

        public void OnExit()
        {
            _encounterManager.Idle.StateEntered -= EncounterIdle;
            
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void StartAdventure()
        {
            Debug.Log($"StartAdventure()...");
            _gameStateDataSource.SetCurrentPath();
            if (_gameStateDataSource.CurrentPathStats == null) return;
            _encounterManager.Idle.Continue();
        }
    }
}