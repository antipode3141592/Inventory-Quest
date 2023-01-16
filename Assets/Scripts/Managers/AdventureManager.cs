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
        IGameManager _gameManager;

        StateMachine _stateMachine;

        AdventureManagerStart _adventureManagerStart;
        Idle _idle;
        Pathfinding _pathfinding;
        Adventuring _adventuring;
        InLocation _inLocation;

        public AdventureManagerStart AdventureManagerStart => _adventureManagerStart;
        public Idle Idle => _idle;
        public Pathfinding Pathfinding => _pathfinding;
        public Adventuring Adventuring => _adventuring;
        public InLocation InLocation => _inLocation;

        public IPath CurrentPath { get; protected set; }
        public ILocation DestinationLocation { get; protected set; }
        public ILocation CurrentLocation { get; protected set; }
        public bool GameEnding { get; set; } = false;

        [Inject]
        public void Init(IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource, IGameManager gameManager)
        {
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
            _gameManager = gameManager;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();
            _adventureManagerStart = new AdventureManagerStart(this);
            _idle = new Idle();
            _pathfinding = new Pathfinding();
            _adventuring = new Adventuring(_encounterManager, _gameStateDataSource);
            _inLocation = new InLocation(this, _gameStateDataSource);

            At(AdventureManagerStart, Idle, () => true);
            At(Idle, Pathfinding, BeginPathfinding());
            At(Pathfinding, Adventuring, BeginAdventure());
            At(Adventuring, InLocation, EndAdventure());
            At(InLocation, Idle, () => true);
            AtAny(AdventureManagerStart, IsGameOver());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> BeginPathfinding() => () => Idle.EndState;
            Func<bool> BeginAdventure() => () => Pathfinding.BeginAdventure;
            Func<bool> EndAdventure() => () => Adventuring.EndAdventure;
            Func<bool> IsGameOver() => () => GameEnding;
        }

        void Start()
        {
            _stateMachine.SetState(Idle);
            _gameManager.OnGameOver += GameOverHandler;
        }

        void GameOverHandler(object sender, EventArgs e)
        {
            GameEnding = true;
        }

        void Update()
        {
            _stateMachine.Tick();
        }
    }
}