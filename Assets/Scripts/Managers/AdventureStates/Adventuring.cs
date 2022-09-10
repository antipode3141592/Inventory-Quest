using Data;
using Data.Encounters;
using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers.States
{
    public class Adventuring : IState
    {
        IEncounterManager _encounterManager;
        IEncounterDataSource _encounterDataSource;
        IPathDataSource _pathDataSource;
        IGameStateDataSource _gameStateDataSource;


        
        public bool EndAdventure = false;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Adventuring(IEncounterManager encounterManager, IEncounterDataSource encounterDataSource, IGameStateDataSource gameStateDataSource)
        {
            _encounterManager = encounterManager;
            _encounterDataSource = encounterDataSource;
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
            _gameStateDataSource.SetCurrentLocation(_gameStateDataSource.DestinationLocation.Stats.Id);
            _gameStateDataSource.SetDestinationLocation("");
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void StartAdventure()
        {
            _gameStateDataSource.SetCurrentPath();
            if (_gameStateDataSource.CurrentPath == null) return;
            _encounterManager.Idle.Continue();
        }
    }
}