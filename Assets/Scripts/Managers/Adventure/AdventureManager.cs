using Data.Encounters;
using Data.Locations;
using FiniteStateMachine;
using InventoryQuest.Audio;
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
        IAudioManager _audioManager;

        StateMachine _stateMachine;

        AdventureManagerStart _adventureManagerStart;
        Pathfinding _pathfinding;
        Adventuring _adventuring;
        InLocation _inLocation;

        public AdventureManagerStart AdventureManagerStart => _adventureManagerStart;
        public Pathfinding Pathfinding => _pathfinding;
        public Adventuring Adventuring => _adventuring;
        public InLocation InLocation => _inLocation;

        public IPath CurrentPath { get; protected set; }
        public ILocation DestinationLocation { get; protected set; }
        public ILocation CurrentLocation { get; protected set; }

        [Inject]
        public void Init(IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource, IGameManager gameManager, IAudioManager audioManager)
        {
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
            _gameManager = gameManager;
            _audioManager = audioManager;
        }

        void Awake()
        {
            _stateMachine = new StateMachine(this);
            _adventureManagerStart = new AdventureManagerStart(_gameManager);
            _pathfinding = new Pathfinding();
            _adventuring = new Adventuring(_encounterManager, _gameStateDataSource, _audioManager);
            _inLocation = new InLocation(_gameStateDataSource, _audioManager);

            At(AdventureManagerStart, InLocation, IsGameBeginning());
            At(InLocation, Pathfinding, BeginPathfinding());
            At(Pathfinding, Adventuring, BeginAdventure());
            At(Adventuring, InLocation, EndAdventure());
            AtAny(AdventureManagerStart, IsGameOver());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> BeginPathfinding() => () => InLocation.EndState;
            Func<bool> BeginAdventure() => () => Pathfinding.EndState;
            Func<bool> EndAdventure() => () => Adventuring.EndAdventure;
            Func<bool> IsGameBeginning() => () => _gameManager.IsGameBegining;
            Func<bool> IsGameOver() => () => _gameManager.IsGameOver;
        }

        void Start()
        {
            _stateMachine.SetState(AdventureManagerStart);
        }

        void Update()
        {
            _stateMachine.Tick();
        }
    }
}