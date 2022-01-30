using Data;
using InventoryQuest.Characters;
using InventoryQuest.UI;
using UnityEngine;
using Zenject;

namespace InventoryQuest
{
    public class GameManager: MonoBehaviour
    {
        IDataSource _dataSource;
        ContainerDisplay containerDisplay;
        Party CurrentParty;
        Party ReserveParty;
        Character Player;
        Character Minion;
        Container LootPile;

        //[Inject]
        //public void Init(IDataSource dataSource)
        //{
        //    _dataSource = dataSource;
        //}

        private void Awake()
        {
            _dataSource = new DataSourceTest();
            Player = CharacterFactory.GetCharacter(_dataSource.GetCharacterStats("Player"), 
                (ContainerStats)_dataSource.GetItemStats("adventure backpack"));
            Minion = CharacterFactory.GetCharacter(_dataSource.GetCharacterStats("Minion"),
                (ContainerStats)_dataSource.GetItemStats("adventure backpack"));
            CurrentParty = new Party(new Character[]{ Player });
            ReserveParty = new Party(new Character[] {});
            LootPile = ContainerFactory.GetContainer((ContainerStats)_dataSource.GetItemStats("loot_pile"));
            
            containerDisplay = FindObjectOfType<ContainerDisplay>();
        }

        private void Start()
        {
            containerDisplay.MyContainer = Player.PrimaryContainer;
        }

        private void Update()
        {
            
        }

        public void AddPieceToLootPile()
        {
            Item item = (Item)ItemFactory.GetItem(_dataSource.GetRandomItemStats(Rarity.common));
            LootPile.TryPlace(item, new Coor(r: 0, c: 0));
        }


    }
}
