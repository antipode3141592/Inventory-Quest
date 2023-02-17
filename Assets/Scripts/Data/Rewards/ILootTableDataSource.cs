using System.Collections.Generic;

namespace Data.Rewards
{
    public interface ILootTableDataSource
    {
        public IList<RarityRange> GetLootTableById(string id);
    }
}
