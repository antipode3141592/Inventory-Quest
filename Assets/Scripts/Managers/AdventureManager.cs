using Data;
using Data.Encounters;
using Data.Locations;
using FiniteStateMachine;
using InventoryQuest.Managers.States;
using System;
using UnityEngine;

using Zenject;

namespace InventoryQuest.Managers
{
    public class AdventureManager : MonoBehaviour, IAdventureManager
    {
        IEncounterManager _encounterManager;
        IGameStateDataSource _gameStateDataSource;
        IEncounterDataSource _encounterDataSource;

        StateMachine _stateMachine;

        Idle _idle;
        Pathfinding _pathfinding;
        Adventuring _adventuring;

        public Idle Idle => _idle;
        public Pathfinding Pathfinding => _pathfinding;
        public Adventuring Adventuring => _adventuring;

        public IPath CurrentPath { get; protected set; }
        public ILocation DestinationLocation { get; protected set; }
        public ILocation CurrentLocation { get; protected set; }

        [Inject]
        public void Init(IEncounterDataSource encounterDataSource, IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource)
        {
            _encounterDataSource = encounterDataSource;
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();
            _idle = new Idle();
            _pathfinding = new Pathfinding();
            _adventuring = new Adventuring(_encounterManager, _gameStateDataSource);

            At(Idle, Pathfinding, BeginPathfinding());
            At(Pathfinding, Adventuring, BeginAdventure());
            At(Adventuring, Idle, EndAdventure());
            

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> BeginPathfinding() => () => Idle.EndState;
            Func<bool> BeginAdventure() => () => Pathfinding.BeginAdventure;
            Func<bool> EndAdventure() => () => Adventuring.EndAdventure;
        }

        void Start()
        {
            _stateMachine.SetState(Idle);
            
        }

        void Update()
        {
            _stateMachine.Tick();
        }


    }
}