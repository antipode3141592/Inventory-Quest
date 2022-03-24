using Data;
using Data.Interfaces;
using Rewired;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class GameManager : MonoBehaviour
    {
        IDataSource _dataSource;
        ContainerDisplayManager _containerDisplayManager;
        PartyManager _partyManager;

        Player player;
        int playerId = 0;

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
        public EventHandler<RotationEventArgs> OnRotateCW;
        public EventHandler<RotationEventArgs> OnRotateCCW;


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
            player = ReInput.players.GetPlayer(playerId);
        }

        private void Start()
        {
            _containerDisplayManager.ConnectLootContainer(LootPile);
            
            StartCoroutine(AddItemsToContainer(2, restPeriod, LootPile, "basic_crossbow_1"));
            StartCoroutine(AddItemsToContainer(1, restPeriod, LootPile, "basic_sword_1"));
            StartCoroutine(AddItemsToContainer(4, restPeriod, LootPile, "apple_fuji"));
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
                    CheckRotateAction();
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
                var newItem = ItemFactory.GetItem(_dataSource.GetItemStats(itemId));

                for (int _c = 0; _c < targetContainer.ContainerSize.column; _c++)
                {
                    if (itemCount >= itemTotal) yield break;
                    

                    if (targetContainer.TryPlace(newItem, new Coor(_r, _c)))
                    {
                        newItem = ItemFactory.GetItem(_dataSource.GetItemStats(itemId));
                        itemCount++;
                    }
                    yield return new WaitForSeconds(restPeriod);
                }
            }
        }

        public void CheckRotateAction()
        {

            bool rotateCW = player.GetButtonUp("RotatePieceCW");
            bool rotateCCW = player.GetButtonUp("RotatePieceCCW");

            if (rotateCW) { 
                var facing = HoldingItem.Shape.Rotate(1); 
                Debug.Log($"CheckRotateAction() detected CW action");
                OnRotateCW?.Invoke(this, new RotationEventArgs(facing));
                return;
            }

            if (rotateCCW) {
                var facing = HoldingItem.Shape.Rotate(-1);
                Debug.Log($"CheckRotateAction() detected CCW action");
                OnRotateCCW?.Invoke(this, new RotationEventArgs(facing));
                return;
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
