using Data;
using Data.Interfaces;
using InventoryQuest.Managers;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace InventoryQuest
{
    public class GameManager : MonoBehaviour
    {
        IDataSource _dataSource;
        ContainerDisplayManager _containerDisplayManager;
        PartyManager _partyManager;

        Container LootPile;

        private IItem holdingItem;
        public IItem HoldingItem 
        { 
            get => holdingItem; 
            set {
                holdingItem = value; 
                if (value is null) OnItemPlaced?.Invoke(this, EventArgs.Empty);
                else OnItemHeld?.Invoke(this, EventArgs.Empty);
                
                
            }
        }

        float restPeriod = 0.1f;

        [SerializeField]
        int targetTotal;

        GameStates currentState = GameStates.Loading;
        

        public GameStates CurrentState { get { return currentState; } }

        public EventHandler OnItemHeld;
        public EventHandler OnItemPlaced;


        [Inject]
        public void Init(ContainerDisplayManager containerDisplayManager, IDataSource dataSource, PartyManager partyManager)
        {
            _containerDisplayManager = containerDisplayManager;
            _dataSource = dataSource;
            _partyManager = partyManager;
        }


        private void Awake()
        {
            LootPile = ContainerFactory.GetContainer((ContainerStats)_dataSource.GetItemStats("loot_pile"));
        }

        private void Start()
        {
            _containerDisplayManager.ConnectLootContainer(LootPile);
            //StartCoroutine(AddItemsToContainer(3, restPeriod, LootPile, "apple_fuji"));
            StartCoroutine(AddItemsToContainer(3, restPeriod, LootPile, "basic_sword_1"));
        }

        private void Update()
        {
            switch (currentState)
            {
                case GameStates.Loading:
                    currentState = GameStates.Default;
                    break;
                case GameStates.Default:
                    break;
                case GameStates.HoldingItem:
                    break;
                default:
                    break;
            }
        }

        public void ChangeState(GameStates targetState)
        {
            if (currentState == targetState) return;
            currentState = targetState;
        }

        public IEnumerator AddItemsToContainer(int itemTotal, float restPeriod, Container targetContainer, string itemId)
        {
            int itemCount = 0;
            for (int _r = 0; _r < targetContainer.ContainerSize.row; _r++)
            {
                for (int _c = 0; _c < targetContainer.ContainerSize.column; _c++)
                {
                    if (itemCount >= itemTotal) yield break;
                    var newItem = ItemFactory.GetItem(_dataSource.GetItemStats(itemId));

                    targetContainer.TryPlace(newItem, new Coor(_r, _c));
                    itemCount++;
                    yield return new WaitForSeconds(restPeriod);
                }
            }
        }

        public void AddPieceToLootPile()
        {
            Item item = (Item)ItemFactory.GetItem(_dataSource.GetRandomItemStats(Rarity.common));
            LootPile.TryPlace(item, new Coor(r: 0, c: 0));
        }
    }

    public enum GameStates { Loading, Default, HoldingItem}
}
