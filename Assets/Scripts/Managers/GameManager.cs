using Data;
using Data.Interfaces;
using InventoryQuest.Characters;
using InventoryQuest.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest
{
    public class GameManager: MonoBehaviour
    {
        IDataSource _dataSource;
        //PartyDisplay _partyDisplay;

        [SerializeField]
        List<ContainerDisplay> characterContainerDisplays;
        [SerializeField]
        ContainerDisplay lootContainerDisplay;
        public Party CurrentParty;
        Character Player;
        Character Minion;
        Container LootPile;

        public IItem HoldingItem;

        float restPeriod = 0.1f;

        [SerializeField]
        int targetTotal;

        GameStates currentState = GameStates.Loading;
        public GameStates CurrentState { get { return currentState; } }

        private void Awake()
        {
            _dataSource = new DataSourceTest();
            
            Player = CharacterFactory.GetCharacter(_dataSource.GetCharacterStats("Player"), 
                (ContainerStats)_dataSource.GetItemStats("adventure backpack"));
            Minion = CharacterFactory.GetCharacter(_dataSource.GetCharacterStats("Minion"),
                (ContainerStats)_dataSource.GetItemStats("small backpack"));
            CurrentParty = new Party(new Character[]{ Player, Minion });
            LootPile = ContainerFactory.GetContainer((ContainerStats)_dataSource.GetItemStats("loot_pile"));

            //_partyDisplay = FindObjectOfType<PartyDisplay>();
            //_partyDisplay.MyParty = CurrentParty;
        }

        private void Start()
        {
            lootContainerDisplay.MyContainer = LootPile;

            characterContainerDisplays[0].MyContainer = CurrentParty.Characters[Player.GuId].PrimaryContainer;
            characterContainerDisplays[1].MyContainer = CurrentParty.Characters[Minion.GuId].PrimaryContainer;

            CurrentParty.Characters[Player.GuId].PrimaryContainer.OnGridUpdated += characterContainerDisplays[0].OnContainerUpdate;
            //CurrentParty.Characters[Minion.GuId].PrimaryContainer.OnGridUpdated += characterContainerDisplays[1].OnContainerUpdate;
            LootPile.OnGridUpdated += lootContainerDisplay.OnContainerUpdate;

            //containerDisplay.MyContainer = Player.PrimaryContainer;
            StartCoroutine(AddItemsToContainer(2, restPeriod, Player.PrimaryContainer, "fuji_apple"));
            StartCoroutine(AddItemsToContainer(3, restPeriod, Minion.PrimaryContainer, "fuji_apple"));
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
            for (int _r = 0; _r < targetContainer.ContainerSize.row; _r++) {
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

        private void OnDisable()
        {
            CurrentParty.Characters[Player.GuId].PrimaryContainer.OnGridUpdated -= characterContainerDisplays[0].OnContainerUpdate;
            //CurrentParty.Characters[Minion.GuId].PrimaryContainer.OnGridUpdated -= characterContainerDisplays[1].OnContainerUpdate;
            LootPile.OnGridUpdated -= lootContainerDisplay.OnContainerUpdate;
        }

        public void AddPieceToLootPile()
        {
            Item item = (Item)ItemFactory.GetItem(_dataSource.GetRandomItemStats(Rarity.common));
            LootPile.TryPlace(item, new Coor(r: 0, c: 0));
        }


    }

    public enum GameStates { Loading, Default, HoldingItem}
}
