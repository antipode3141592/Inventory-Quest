using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using UnityEngine;

namespace InventoryQuest
{
    public class LootContainerController
    {
        IDataSource DataSource;
        public Container MyContainer;

        public LootContainerController(IDataSource dataSource, Container myContainer)
        {
            DataSource = dataSource;
            MyContainer = myContainer;
        }

        

        public bool AddRandomLootToContainer(Rarity rarity)
        {
            Item loot = DataSource.GetRandomItem(rarity: rarity);
            Vector2Int currentGuess = new Vector2Int(0,0);
            for (int j = 0; j < MyContainer.Size.y; j++)
            {
                currentGuess.y = j;
                for (int i = 0; i < MyContainer.Size.x; i++)
                {
                    currentGuess.x = i;
                    if (MyContainer.TryPlace(loot, currentGuess))
                    {
                        return true;
                    }
                }
            }
                
                
            return false;
        }

        public void FillContainerWithRandomLoot()
        {
            
        }
    }

    public enum Rarity { common, uncommon, rare, epic, legendary }

    public class LootTable
    {
        public RarityRange[] RarityRanges = 
        {
            new RarityRange(Rarity.common, 0f, 0.5f),
            new RarityRange(Rarity.uncommon, 0.5f, 0.9f),
            new RarityRange(Rarity.rare, 0.90f, 1f)
        };

        public static Rarity GetRandomRarity()
        {


            return Rarity.common;
        }
    }

    public class RarityRange
    {
        public Rarity Rarity;
        public float Min;
        public float Max;

        public RarityRange(Rarity rarity, float min, float max)
        {
            Rarity = rarity;
            Min = min;
            Max = max;
        }
    }

    
}
