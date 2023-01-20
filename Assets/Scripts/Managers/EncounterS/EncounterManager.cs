using Data;
using FiniteStateMachine;
using InventoryQuest.Managers.States;
using InventoryQuest.Traveling;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class EncounterManager : MonoBehaviour, IEncounterManager, IDeltaTimeTracker
    {
        [SerializeField] TravelSettings travelSettings;

        Queue<EncounterModifier> encounterModifiers = new();

        //managers
        IPartyManager _partyManager;
        IRewardManager _rewardManager;
        IPenaltyManager _penaltyManager;
        IPartyController _partyController;
        IGameStateDataSource _gameStateDataSource;
        IInputManager _inputManager;
        IGameManager _gameManager;
        
        // time tracking
        float _deltaTime;
        public float DeltaTime => _deltaTime;

        public bool GameBeginning { get; set; } = false;
        public bool GameEnding { get; set; } = false;

        // state machine
        StateMachine _stateMachine;

        EncounterManagerStart _start;
        Idle _idle;
        Wayfairing _wayfairing;
        Loading _loading;
        Resolving _resolving;
        CleaningUp _cleaningUp;

        public EncounterManagerStart START => _start;
        public Idle Idle => _idle;
        public Wayfairing Wayfairing => _wayfairing;
        public Loading Loading => _loading;
        public Resolving Resolving => _resolving;
        public CleaningUp CleaningUp => _cleaningUp;

        public string CurrentStateName => _stateMachine.CurrentStateName;

        public Queue<EncounterModifier> EncounterModifiers => encounterModifiers;

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IPenaltyManager penaltyManager, IPartyController partyController, IGameStateDataSource gameStateDataSource, IInputManager inputManager, IGameManager gameManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _penaltyManager = penaltyManager;
            _partyController = partyController;
            _gameStateDataSource = gameStateDataSource;
            _inputManager = inputManager;
            _gameManager = gameManager;
        }

        void Awake()
        {
            _stateMachine = new StateMachine(this);

            _start = new EncounterManagerStart(this);
            _idle = new Idle();
            _wayfairing = new Wayfairing(partyController: _partyController, deltaTimeTracker: this, travelSettings: travelSettings);
            _loading = new Loading(gameStateDataSource: _gameStateDataSource);
            _resolving = new Resolving(rewardManager: _rewardManager, penaltyManager: _penaltyManager, partyManager: _partyManager, gameStateDataSource: _gameStateDataSource, deltaTimeTracker: this);
            _cleaningUp = new CleaningUp(rewardManager: _rewardManager, gameStateDataSource: _gameStateDataSource, inputManager: _inputManager, encounterManager: this);

            At(_start, _idle, Starting());
            At(_idle, _wayfairing, BeginWayfairing());
            At(_wayfairing, _loading, WayfairingComplete());
            At(_loading, _resolving, LoadingComplete());
            At(_resolving, _cleaningUp, IsResolvingComplete());
            At(_cleaningUp, _idle, IsCleaningUpComplete());
            At(_cleaningUp, _wayfairing, IsNextEncounterAvailable());
            //AtAny(_start, IsGameBeginning());
            AtAny(_start, IsGameOver());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> Starting() => () => true;
            Func<bool> BeginWayfairing() => () => _idle.EndState;
            Func<bool> WayfairingComplete() => () => _wayfairing.IsDone;
            Func<bool> LoadingComplete() => () => _loading.IsLoaded;
            Func<bool> IsResolvingComplete() => () => _resolving.EndState && _resolving.IsDone;
            Func<bool> IsCleaningUpComplete() => () => _cleaningUp.EndState && !_cleaningUp.EncounterAvailable;
            Func<bool> IsNextEncounterAvailable() => () => _cleaningUp.EndState && _cleaningUp.EncounterAvailable;
            //Func<bool> IsGameBeginning() => () => GameBeginning;
            Func<bool> IsGameOver() => () => GameEnding;
        }

        void Start()
        {
            _stateMachine.SetState(Idle);
            //_gameManager.OnGameBegining += OnGameBeginningHandler;
            _gameManager.OnGameOver += OnGameOverHandler;
            _inputManager.OnEncounterModifierAdded += EncounterModiferAdded;
        }

        void EncounterModiferAdded(object sender, EncounterModifier modifier)
        {
            AddEncounterModifier(modifier);
        }

        void OnGameOverHandler(object sender, EventArgs e)
        {
            GameEnding = true;
        }

        void OnGameBeginningHandler(object sender, EventArgs e)
        {
            GameBeginning = true;
        }

        void Update()
        {
            _deltaTime = Time.deltaTime;
            _stateMachine.Tick();
        }

        public void AddEncounterModifier(EncounterModifier encounterModifier)
        {
            encounterModifiers.Enqueue(encounterModifier);
        }

        
    }
}
