using Data.Items;
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

        public IDictionary<string, Container> Piles { get; } = new Dictionary<string, Container>();
        List<IItem> deleteItems = new List<IItem>();

        public event EventHandler OnHarvestCleared;
        public event EventHandler<Container> OnPileSelected;

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
            var harvestPile = (Container)ItemFactory.GetItem((ContainerStats)_itemDataSource.GetItemStats(containerId));
            Piles.Add(harvestPile.GuId, harvestPile);
            for (int i = 0; i < quantity; i++)
            {
                ItemPlacementHelpers.TryAutoPlaceToContainer(harvestPile, ItemFactory.GetItem(_itemDataSource.GetItemStats(itemId)));
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
                deleteItems.Add(container);
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

    public enum HarvestTypes { Forest, Mine, Herbs }
}
