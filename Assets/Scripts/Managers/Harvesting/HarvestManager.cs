using Data.Items;
using FiniteStateMachine;
using InventoryQuest.Managers.States;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class HarvestManager: SerializedMonoBehaviour, IHarvestManager
    {
        [SerializeField] IItemStats woodHarvestSawStats;
        [SerializeField] List<IItemStats> woodHarvestCutItemStats;

        IItemDataSource _itemDataSource;

        HarvestTypes currentHarvestType;

        StateMachine _stateMachine;

        Idle _idle;
        LoadingHarvest _loadingHarvest;
        Harvesting _harvesting;
        ResolvingHarvest _resolvingHarvest;
        CleaningUpHarvest _cleaningUpHarvest;

        public string SelectedPileId;

        public Idle Idle => _idle;
        public LoadingHarvest LoadingHarvest => _loadingHarvest;
        public Harvesting Harvesting => _harvesting;
        public ResolvingHarvest ResolvingHarvest => _resolvingHarvest;
        public CleaningUpHarvest CleaningUpHarvest => _cleaningUpHarvest;

        public HarvestTypes CurrentHarvestType => currentHarvestType;

        public IDictionary<string, IContainer> Piles { get; } = new Dictionary<string, IContainer>();
        readonly List<IItem> deleteItems = new();

        public event EventHandler OnHarvestCleared;
        public event EventHandler<IContainer> OnPileSelected;

        [Inject]
        public void Init(IItemDataSource itemDataSource)
        {
            _itemDataSource = itemDataSource;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();

            _idle = new Idle();
            _loadingHarvest = new LoadingHarvest(this);
            _harvesting = new Harvesting();
            _resolvingHarvest = new ResolvingHarvest();
            _cleaningUpHarvest = new CleaningUpHarvest(this);

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

        public void PopulateHarvest(string containerId, string itemId, int quantity)
        {
            switch (currentHarvestType)
            {
                case HarvestTypes.Forest:
                    PopulateWoodHarvest(containerId, itemId, quantity);
                    break;
                default:
                    break;
            }
        }

        void PopulateWoodHarvest(string containerId, string itemId, int quantity)
        {
            //add woodsaw
            var woodHarvestSaw = ItemFactory.AddComponents(woodHarvestSawStats, new WoodHarvestSaw(woodHarvestSawStats)) as WoodHarvestSaw;
            var woodHarvestSawContainer = woodHarvestSaw.Components[typeof(IContainer)] as IContainer;
            woodHarvestSaw
                .SubscribeToContainerEvents()
                .SetCutItemDictionary(woodHarvestCutItemStats);
            Piles.Add(woodHarvestSawContainer.GuId, woodHarvestSawContainer);

            //add empty wood log pile(s)
            var harvestPile = ItemFactory.GetItem(_itemDataSource.GetById(containerId));
            if (!harvestPile.Components.ContainsKey(typeof(IContainer))) return;
            IContainer harvestContainer = harvestPile.Components[typeof(IContainer)] as IContainer;
            if (harvestContainer is null) return;
            Piles.Add(harvestContainer.GuId, harvestContainer);

            //add logs to log pile
            for (int i = 0; i < quantity; i++)
            {
                ItemPlacementHelpers.TryAutoPlaceToContainer(harvestContainer, ItemFactory.GetItem(_itemDataSource.GetById(itemId)));
            }
        }

        public void DestroyHarvest()
        {
            deleteItems.Clear();
            foreach (var container in Piles.Values)
            {
                foreach (var content in container.Contents.Values)
                {
                    deleteItems.Add(content.Item);
                }
                deleteItems.Add(container.Item);
            }

            for (int i = 0; i < deleteItems.Count; i++)
            {
                deleteItems[i] = null;
            }
            Piles.Clear();
            OnHarvestCleared?.Invoke(this, EventArgs.Empty);
        }

        public void SelectPile(string containerGuid)
        {
            if (Piles.ContainsKey(containerGuid))
            {
                SelectedPileId = containerGuid;
                OnPileSelected?.Invoke(this, Piles[containerGuid]);
            }
        }
    }
}
