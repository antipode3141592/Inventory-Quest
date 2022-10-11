using FiniteStateMachine;
using InventoryQuest.Managers.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class HarvestManager: MonoBehaviour, IHarvestManager
    {
        IRewardManager _rewardManager;

        HarvestTypes currentHarvestType;

        StateMachine _stateMachine;

        Idle _idle;
        LoadingHarvest _loadingHarvest;
        Harvesting _harvesting;
        ResolvingHarvest _resolvingHarvest;
        CleaningUpHarvest _cleaningUpHarvest;

        public Idle Idle => _idle;
        public LoadingHarvest LoadingHarvest => _loadingHarvest;
        public Harvesting Harvesting => _harvesting;
        public ResolvingHarvest ResolvingHarvest => _resolvingHarvest;
        public CleaningUpHarvest CleaningUpHarvest => _cleaningUpHarvest;

        public HarvestTypes CurrentHarvestType => currentHarvestType;

        [Inject]
        public void Init(IRewardManager rewardManager)
        {
            _rewardManager = rewardManager;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();

            _idle = new Idle();
            _loadingHarvest = new LoadingHarvest(_rewardManager, this);
            _harvesting = new Harvesting();
            _resolvingHarvest = new ResolvingHarvest();
            _cleaningUpHarvest = new CleaningUpHarvest(_rewardManager);

            At(_idle, _loadingHarvest, LoadHarvest());
            At(_loadingHarvest, _harvesting, HarvestPopulated());
            At(_harvesting, _resolvingHarvest, HarvestComplete());
            At(_resolvingHarvest, _cleaningUpHarvest, HarvestResolved());
            At(_cleaningUpHarvest, _idle, CleanUpComplete());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> LoadHarvest() => () => _idle.EndState;
            Func<bool> HarvestPopulated() => () => _loadingHarvest.IsDone;
            Func<bool> HarvestComplete() => () => _harvesting.IsDone;
            Func<bool> HarvestResolved() => () => _resolvingHarvest.IsDone;
            Func<bool> CleanUpComplete() => () => _cleaningUpHarvest.IsDone;

        }

        void Start()
        {
            _stateMachine.SetState(Idle);
        }

        void Update()
        {
            _stateMachine.Tick();
        }

        public void BeginHarvest(HarvestTypes harvestType)
        {
            currentHarvestType = harvestType;
            _idle.Continue();
        }
    }

    public enum HarvestTypes { Forest, Mine, Herbs }
}
