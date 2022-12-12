using Data.Items;
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
}
