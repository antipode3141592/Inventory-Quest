using Data;
using InventoryQuest.Characters;
using UnityEngine;
using Zenject;

namespace InventoryQuest
{
    public class GameManager: MonoBehaviour
    {
        IDataSource _dataSource;
        Character Player;
        Container LootPile;

        [Inject]
        public void Init(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private void Awake()
        {

            //initialize player character with base stats and standard 10x5 unit backpack
            CharacterStats playerStats = new CharacterStats(10f,10f,10f);
            ItemStats backpackStats = new ItemStats("adventure backpack", 
                weight: 2f, 
                goldValue: 5f, 
                description: "a basic adventurer's backpack" );
            Player = new Character(ContainerFactory.GetContainer(ShapeType.Square1, backpackStats, new Coor(r: 5, c: 10)), playerStats);

            ItemStats lootPile = new ItemStats("loot pile",
                weight: 0f,
                goldValue: 0f,
                description: "current loot pile");
            LootPile = ContainerFactory.GetContainer(ShapeType.Square1, lootPile, new Coor(r: 3, c: 5));

        }

        public void AddPieceToLootPile()
        {
            Item item = _dataSource.GetRandomItem(Rarity.common);
            LootPile.TryPlace(item, new Coor(r: 0, c: 0));
        }


    }
}
