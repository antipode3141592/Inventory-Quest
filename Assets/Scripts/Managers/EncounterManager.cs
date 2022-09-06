using Data;
using Data.Encounters;
using FiniteStateMachine;
using InventoryQuest.Managers.States;
using InventoryQuest.Traveling;
using System;
using UnityEngine;
using Zenject;


namespace InventoryQuest.Managers
{
    public class EncounterManager : MonoBehaviour, IEncounterManager
    {
        IPartyManager _partyManager;
        IRewardManager _rewardManager;
        IPartyController _partyController;
        IGameStateDataSource _gameStateDataSource;

        StateMachine _stateMachine;

        Idle _idle;
        Wayfairing _wayfairing;
        Loading _loading;
        Preparing _preparing;
        Resolving _resolving;
        CleaningUp _cleaningUp;

        public Idle Idle => _idle;
        public Wayfairing Wayfairing => _wayfairing;
        public Loading Loading => _loading;
        public Preparing Preparing => _preparing;
        public Resolving Resolving => _resolving;
        public CleaningUp CleaningUp => _cleaningUp;

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IPartyController partyController, IGameStateDataSource gameStateDataSource)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _partyController = partyController;
            _gameStateDataSource = gameStateDataSource;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();

            _idle = new Idle();
            _wayfairing = new Wayfairing(partyController: _partyController);
            _loading = new Loading(_gameStateDataSource);
            _preparing = new Preparing(partyController: _partyController);
            _resolving = new Resolving(rewardManager: _rewardManager, partyManager: _partyManager, gameStateDataSource: _gameStateDataSource);
            _cleaningUp = new CleaningUp();


            At(_idle, _wayfairing, BeginWayfairing());
            At(_wayfairing, _loading, TargetDistanceMoved());
            At(_loading, _preparing, IsLoadingComplete());
            At(_preparing, _resolving, IsPreparingComplete());
            At(_resolving, _cleaningUp, IsResolvingComplete());
            At(_cleaningUp, _idle, IsCleaningUpComplete());
            At(_cleaningUp, _wayfairing, IsNextEncounterAvailable());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> BeginWayfairing() => () => _idle.EndState;
            Func<bool> TargetDistanceMoved() => () => _wayfairing.TotalDistanceMoved >= 30;
            Func<bool> IsLoadingComplete() => () => _loading.IsLoaded;
            Func<bool> IsPreparingComplete() => () => _preparing.EndState;
            Func<bool> IsResolvingComplete() => () => _resolving.EndState;
            Func<bool> IsCleaningUpComplete() => () => _cleaningUp.EndState && !_cleaningUp.EncounterAvailable;
            Func<bool> IsNextEncounterAvailable() => () => _cleaningUp.EndState && _cleaningUp.EncounterAvailable;
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
