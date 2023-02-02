using UnityEngine;
using Data.Items;

namespace Data.Penalties
{
    public class ItemLossPenaltyStats : IItemLossPenaltyStats
    {
        [SerializeField] int quantityToLose;
        [SerializeField] IItemStats itemToLose;

        public int QuantityToLose => quantityToLose;
        public IItemStats ItemToLose => itemToLose;
    }
}
