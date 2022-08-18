using Data;
using Data.Encounters;
using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class Adventuring : IState
    {
        IEncounterManager _encounterManager;
        IEncounterDataSource _encounterDataSource;
        IPathDataSource _pathDataSource;
        IGameStateDataSource _gameStateDataSource;

        int currentIndex;
        
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
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void StartAdventure()
        {
            _gameStateDataSource.SetCurrentPath();
            if (_gameStateDataSource.CurrentPath == null) return;
            StartPath();
        }

        void StartPath()
        {
            currentIndex = 0;
            LoadEncounter(_gameStateDataSource.CurrentPath.EncounterIds[currentIndex]);
        }

        void LoadEncounter(string id)
        {
            if (id == string.Empty)
                _encounterManager.CurrentEncounter = EncounterFactory.GetEncounter(_encounterDataSource.GetRandomEncounter());
            _encounterManager.CurrentEncounter = EncounterFactory.GetEncounter(_encounterDataSource.GetEncounterById(id));
        }

        void OnEncounterCompleteHandler(object sender, string e)
        {
            BeginNextEncounter();
        }

        public void BeginNextEncounter()
        {
            if (currentIndex < _gameStateDataSource.CurrentPath.Length - 1)
            {
                currentIndex++;

                var nextEncounterId = _gameStateDataSource.CurrentPath.EncounterIds[currentIndex];
                LoadEncounter(nextEncounterId);
            }
            else
            {
                EndAdventure = true;
            }
        }
    }
}