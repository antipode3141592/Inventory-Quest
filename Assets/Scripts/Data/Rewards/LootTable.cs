using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Data.Rewards
{
    public class LootTable
    {
        ILootTableDataSource _dataSource;

        public LootTable(ILootTableDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public Rarity GetRandomRarity(string lootTableId)
        {
            float random = Random.Range(0f, 1f);
            foreach(var range in _dataSource.GetLootTableById(lootTableId))
            {
                if (range.Min <= random && random <= range.Max) return range.Rarity;
            }
            return Rarity.common;
        }
    }

    public class LootTableDataSourceTest : ILootTableDataSource
    {
        Dictionary<string, IList<RarityRange>> lootTables = new Dictionary<string, IList<RarityRange>>()
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

    public interface ILootTableDataSource
    {
        public IList<RarityRange> GetLootTableById(string id);
    }
}
