using Data.Health;
using Data.Items;

namespace Data.Penalties
{
    public interface IItemLossPenaltyStats : IPenaltyStats
    {
        public int QuantityToLose { get; }
        public IItemStats ItemToLose { get; }
    }
}
