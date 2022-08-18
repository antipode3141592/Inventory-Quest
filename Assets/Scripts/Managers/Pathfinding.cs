using Data;
using Data.Encounters;
using FiniteStateMachine;
using System;

namespace InventoryQuest.Managers
{
    public class Pathfinding : IState
    {
        IEncounterManager _encounterManager;
        IEncounterDataSource _encounterDataSource;
        IGameStateDataSource _gameStateDataSource;

        public bool BeginAdventure = false;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Pathfinding(IEncounterManager encounterManager, IEncounterDataSource encounterDataSource, IGameStateDataSource gameStateDataSource)
        {
            _encounterManager = encounterManager;
            _encounterDataSource = encounterDataSource;
            _gameStateDataSource = gameStateDataSource;
        }

        public void OnEnter()
        {
            BeginAdventure = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void ChoosePath()
        {
            
            BeginAdventure = true;
        }
    }
}