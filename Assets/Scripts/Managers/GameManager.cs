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

        float restPeriod = 1f;
        float restTimer = 0f;

        int _r = 0;
        int _c = 0;

        [SerializeField]
        int targetTotal = 15;
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

            Player.PrimaryContainer.OnGridUpdated += containerDisplay.OnContainerUpdate;
        }

        private void Start()
        {
            containerDisplay.MyContainer = Player.PrimaryContainer;
        }

        private void Update()
        {
            restTimer += Time.deltaTime;
            if (restTimer >= restPeriod) { 
                restTimer = 0f;
                if (Player.PrimaryContainer.Contents.Count < targetTotal)
                {
                    var newItem = ItemFactory.GetItem(_dataSource.GetItemStats("apple_fuji"));
                    Player.PrimaryContainer.TryPlace(newItem, new Coor(_r, _c));
                    if (_c < Player.PrimaryContainer.ContainerSize.column-1)
                    {
                        _c++;
                    }
                    else
                    {
                        _c = 0;
                        _r++;
                    }
                }
            }
            
            
        }

        private void OnDisable()
        {
            Player.PrimaryContainer.OnGridUpdated -= containerDisplay.OnContainerUpdate;
        }

        public void AddPieceToLootPile()
        {
            Item item = (Item)ItemFactory.GetItem(_dataSource.GetRandomItemStats(Rarity.common));
            LootPile.TryPlace(item, new Coor(r: 0, c: 0));
        }


    }
}
