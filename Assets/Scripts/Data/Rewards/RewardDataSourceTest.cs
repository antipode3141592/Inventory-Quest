using System.Collections.Generic;

namespace Data.Rewards
{
    public class RewardDataSourceTest : IRewardDataSource
    {
        Dictionary<string, IRewardStats> rewardDictionary = new()
        {
            { "ring_charisma", new ItemRewardStats(
                id: "ring_charisma",
                name: "Ring of Charisma",
                description: "A simple silver band that is quite charming.",
                itemId: "ring_charisma_5"
                )
            },
            {
                "common_loot_pile_small", new RandomItemRewardStats(
                    id: "common_loot_pile_small",
                    name: "Common loot pile",
                    description: "Common loot pile",
                    lootContainerId: "loot_pile_small",
                    lootTableId: "common_loot"
                    )
            },
            {
                "common_loot_pile_medium", new RandomItemRewardStats(
                    id: "common_loot_pile_medium",
                    name: "Common loot pile",
                    description: "Common loot pile",
                    lootContainerId: "loot_pile_medium",
                    lootTableId: "common_loot"
                    )
            },
            {
                "common_loot_pile_large", new RandomItemRewardStats(
                    id: "common_loot_pile_large",
                    name: "Common loot pile",
                    description: "Common loot pile",
                    lootContainerId: "loot_pile_large",
                    lootTableId: "common_loot"
                    )
            },
            {
                "uncommon_loot_pile_gigantic",
                new RandomItemRewardStats(
                    id: "uncommon_loot_pile_gigantic",
                    name: "Common loot pile",
                    description: "Common loot pile",
                    lootContainerId: "loot_pile_gigantic",
                    lootTableId: "uncommon_loot"
                    )
            },
            {
                "spirit_ring", new ItemRewardStats(
                    id: "spirit_ring",
                    name: "Greater Spirit Ring",
                    description: "A ring imbued with powerful spirit magic",
                    itemId: "ring_spirit_25"
                    )
            },
            {
                "power_sword", new ItemRewardStats(
                    id: "power_sword",
                    name: "Powerful Sword",
                    description: "A magical sword that increases the strength of the bearer.",
                    itemId: "basic_sword_15"
                    )
            },
            {
                "shortsword_0", new ItemRewardStats(
                    id: "shortsword_0",
                    name: "Steel Shortsword",
                    description: "A solidly built short sword.  Simple, but effective.",
                    itemId: "basic_shortsword_0"
                    )
            }

        };

        public IRewardStats GetRewardById(string id)
        {
            if (rewardDictionary.ContainsKey(id)) return rewardDictionary[id];
            return null;
        }
    }
}
