using UnityEngine;

namespace Data.Rewards
{
    public class ItemRewardStats: IRewardStats
    {
        [SerializeField] string _itemId;

        public ItemRewardStats()
        {

        }

        public ItemRewardStats(string itemId)
        {
            _itemId = itemId;
        }

        public string ItemId => _itemId;
    }
}
