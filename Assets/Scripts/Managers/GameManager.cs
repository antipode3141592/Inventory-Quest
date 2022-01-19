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
            ContainerStats backpackStats = new ContainerStats(itemId:"adventure backpack", 
                weight: 2f, 
                goldValue: 5f, 
                description: "a basic adventurer's backpack",
                containerSize: new Coor(r: 5,c: 12));
            Player = CharacterFactory.GetCharacter(playerStats, backpackStats);

            ContainerStats lootPile = new ContainerStats("loot pile",
                weight: 0f,
                goldValue: 0f,
                description: "current loot pile",
                containerSize: new Coor(r: 3, c: 5));
            LootPile = ContainerFactory.GetContainer(lootPile);

        }

        public void AddPieceToLootPile()
        {
            Item item = _dataSource.GetRandomItem(Rarity.common);
            LootPile.TryPlace(item, new Coor(r: 0, c: 0));
        }


    }
}
