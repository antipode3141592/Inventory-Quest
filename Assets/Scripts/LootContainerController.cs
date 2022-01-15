using Data;

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
            Coor currentGuess = new Coor(0,0);
            for (int r = 0; r < MyContainer.ContainerSize.row; r++)
            {
                currentGuess.row = r;
                for (int c = 0; c < MyContainer.ContainerSize.column; c++)
                {
                    currentGuess.column = c;
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
