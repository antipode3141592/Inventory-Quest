using Data;
using InventoryQuest.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CharacterStats playerStats = new CharacterStats(10,10,10);
            ItemStats backpackStats = new ItemStats("adventure backpack", 
                weight: 2f, 
                goldValue: 5f, 
                description: "a basic adventurer's backpack" );
            Player = new Character(new Container(backpackStats, new Vector2Int(x: 10, y: 5)), playerStats);

            ItemStats lootPile = new ItemStats("loot pile",
                weight: 0f,
                goldValue: 0f,
                description: "current loot pile");
            LootPile = new Container(lootPile, new Vector2Int(x: 5, y: 3));

        }

        public void AddPieceToLootPile()
        {
            Item item = _dataSource.GetRandomItem(Rarity.common);
            LootPile.TryPlace(item, new Vector2Int(x: 0, y: 0));
        }


    }
}
