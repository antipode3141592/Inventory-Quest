using System.Collections.Generic;

namespace Data.Rewards
{
    public class LootTableDataSourceTest : ILootTableDataSource
    {
        Dictionary<string, IList<RarityRange>> lootTables = new()
        {
            {
                "common_loot", new RarityRange[]
                {
                    new RarityRange(Rarity.common, 0f, 0.7f),
                    new RarityRange(Rarity.uncommon, 0.7f, 0.95f),
                    new RarityRange(Rarity.rare, 0.95f, 1f)
                }
            },
            {
                "uncommon_loot", new RarityRange[]
                {
                    new RarityRange(Rarity.common, 0f, 0.5f),
                    new RarityRange(Rarity.uncommon, 0.5f, 0.9f),
                    new RarityRange(Rarity.rare, 0.9f, 1f)
                }
            },
            {
                "rare_loot", new RarityRange[]
                {
                    new RarityRange(Rarity.common, 0f, 0.4f),
                    new RarityRange(Rarity.uncommon, 0.4f, 0.7f),
                    new RarityRange(Rarity.rare, 0.7f, 0.95f),
                    new RarityRange(Rarity.epic, 0.95f, 1f)
                }
            }
        };

        public IList<RarityRange> GetLootTableById(string id)
        {
            if (lootTables.ContainsKey(id)) return lootTables[id];
            return null;
        }
    }
}
