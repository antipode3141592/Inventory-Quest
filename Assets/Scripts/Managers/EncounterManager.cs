using Data;
using Data.Encounters;
using FiniteStateMachine;
using InventoryQuest.Managers.States;
using InventoryQuest.Traveling;
using System;
using System.Collections;
using UnityEngine;
using Zenject;


namespace InventoryQuest.Managers
{
    public class EncounterManager : MonoBehaviour, IEncounterManager
    {
        [SerializeField] TravelSettings travelSettings;

        IPartyManager _partyManager;
        IRewardManager _rewardManager;
        IPenaltyManager _penaltyManager;
        IPartyController _partyController;
        IGameStateDataSource _gameStateDataSource;

        StateMachine _stateMachine;

        public string CurrentStateName => _stateMachine.CurrentStateName;

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

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IPenaltyManager penaltyManager, IPartyController partyController, IGameStateDataSource gameStateDataSource)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _penaltyManager = penaltyManager;
            _partyController = partyController;
            _gameStateDataSource = gameStateDataSource;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();

            _idle = new Idle();
            _wayfairing = new Wayfairing(partyController: _partyController);
            _loading = new Loading(gameStateDataSource: _gameStateDataSource, partyController: _partyController);
            _managingInventory = new ManagingInventory(partyController: _partyController);
            _resolving = new Resolving(rewardManager: _rewardManager, penaltyManager: _penaltyManager, partyManager: _partyManager, gameStateDataSource: _gameStateDataSource);
            _cleaningUp = new CleaningUp(rewardManager: _rewardManager, gameStateDataSource: _gameStateDataSource);


            At(_idle, _wayfairing, BeginWayfairing());
            At(_wayfairing, _loading, TargetDistanceMoved());
            At(_loading, _managingInventory, IsLoadingComplete());
            At(_loading, _resolving, SkipInventoryStep());
            At(_managingInventory, _resolving, IsPreparingComplete());
            At(_resolving, _cleaningUp, IsResolvingComplete());
            At(_cleaningUp, _idle, IsCleaningUpComplete());
            At(_cleaningUp, _wayfairing, IsNextEncounterAvailable());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> BeginWayfairing() => () => _idle.EndState;
            Func<bool> TargetDistanceMoved() => () => _wayfairing.TotalDistanceMoved >= travelSettings.DefaultDistanceBetweenEncounters;
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
            _resolving.OnEncounterResolveStart += BeginAlertTimer;
        }

        void Update()
        {
            _stateMachine.Tick();
        }

        void BeginAlertTimer(object sender, float e)
        {
            StartCoroutine(Countdown(e));
        }

        IEnumerator Countdown(float time)
        {
            yield return new WaitForSeconds(time);
            _resolving.CompleteResolution();
        }
    }
}
