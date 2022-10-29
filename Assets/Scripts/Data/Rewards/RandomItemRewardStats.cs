using UnityEngine;

namespace Data.Rewards
{
    public class RandomItemRewardStats : IRewardStats
    {
        [SerializeField] string _lootContainerId;
        [SerializeField] string _lootTableId;

        public RandomItemRewardStats()
        {
            _lootContainerId = "loot_pile_small";
            _lootTableId = "common_loot";
        }

        public RandomItemRewardStats(string lootContainerId, string lootTableId)
        {
            _lootContainerId = lootContainerId;
            _lootTableId = lootTableId;
        }

        public string LootContainerId => _lootContainerId;

        public string LootTableId => _lootTableId;
    }
}