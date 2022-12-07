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

        List<EncounterModifier> encounterModifiers = new();

        //managers
        IPartyManager _partyManager;
        IRewardManager _rewardManager;
        IPenaltyManager _penaltyManager;
        IPartyController _partyController;
        IGameStateDataSource _gameStateDataSource;
        IInputManager _inputManager;
        
        // time tracking
        float _deltaTime;
        public float DeltaTime => _deltaTime;

        // state machine
        StateMachine _stateMachine;

        Idle _idle;
        Wayfairing _wayfairing;
        Loading _loading;
        ManagingInventory _managingInventory;
        Resolving _resolving;
        CleaningUp _cleaningUp;

        public Idle Idle => _idle;
        public Wayfairing Wayfairing => _wayfairing;
        public Loading Loading => _loading;
        public ManagingInventory ManagingInventory => _managingInventory;
        public Resolving Resolving => _resolving;
        public CleaningUp CleaningUp => _cleaningUp;

        public string CurrentStateName => _stateMachine.CurrentStateName;

        public List<EncounterModifier> EncounterModifiers => encounterModifiers;

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IPenaltyManager penaltyManager, IPartyController partyController, IGameStateDataSource gameStateDataSource, IInputManager inputManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _penaltyManager = penaltyManager;
            _partyController = partyController;
            _gameStateDataSource = gameStateDataSource;
            _inputManager = inputManager;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();

            _idle = new Idle();
            _wayfairing = new Wayfairing(partyController: _partyController, deltaTimeTracker: this, travelSettings: travelSettings);
            _loading = new Loading(gameStateDataSource: _gameStateDataSource);
            _managingInventory = new ManagingInventory(partyController: _partyController, inputManager: _inputManager) ;
            _resolving = new Resolving(rewardManager: _rewardManager, penaltyManager: _penaltyManager, partyManager: _partyManager, gameStateDataSource: _gameStateDataSource, deltaTimeTracker: this);
            _cleaningUp = new CleaningUp(rewardManager: _rewardManager, gameStateDataSource: _gameStateDataSource, inputManager: _inputManager, encounterManager: this);

            At(_idle, _wayfairing, BeginWayfairing());
            At(_wayfairing, _loading, WayfairingComplete());
            At(_loading, _managingInventory, IsLoadingComplete());
            At(_loading, _resolving, SkipInventoryStep());
            At(_managingInventory, _resolving, IsPreparingComplete());
            At(_resolving, _cleaningUp, IsResolvingComplete());
            At(_cleaningUp, _idle, IsCleaningUpComplete());
            At(_cleaningUp, _wayfairing, IsNextEncounterAvailable());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> BeginWayfairing() => () => _idle.EndState;
            Func<bool> WayfairingComplete() => () => _wayfairing.IsDone;
            Func<bool> IsLoadingComplete() => () => _loading.ManageInventory;
            Func<bool> SkipInventoryStep() => () => _loading.IsLoaded;
            Func<bool> IsPreparingComplete() => () => _managingInventory.EndState;
            Func<bool> IsResolvingComplete() => () => _resolving.EndState && _resolving.IsDone;
            Func<bool> IsCleaningUpComplete() => () => _cleaningUp.EndState && !_cleaningUp.EncounterAvailable;
            Func<bool> IsNextEncounterAvailable() => () => _cleaningUp.EndState && _cleaningUp.EncounterAvailable;
        }

        void Start()
        {
            _stateMachine.SetState(Idle);
        }

        void Update()
        {
            _deltaTime = Time.deltaTime;
            _stateMachine.Tick();
        }

        public void AddEncounterModifier(EncounterModifier encounterModifier)
        {
            if (encounterModifiers is null || encounterModifiers.Count == 0)
                encounterModifiers = new();
            encounterModifiers.Add(encounterModifier);
        }
    }
}
