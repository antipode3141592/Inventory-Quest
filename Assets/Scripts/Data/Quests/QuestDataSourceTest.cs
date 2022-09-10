using System.Collections.Generic;

namespace Data.Quests
{
    public class QuestDataSourceTest : IQuestDataSource
    {
        Dictionary<string, IQuestStats> quests = new()
        {
            {"quest_000", new GatheringQuestStats(
                id: "quest_000",
                name: "get_apples",
                description: "gather 5 fuji apples",
                experience: 500,
                rewardId: "ring_charisma_1",
                sourceId: "",
                sourceType: QuestSourceTypes.Character,
                sinkId: "",
                sinkType: QuestSourceTypes.Character,
                targetQuantity: 5,
                targetItemId: "apple_fuji"
                ) 
            },
            {"quest_intro_delivery", new DeliveryQuestStats(
                id: "quest_intro_delivery",
                name: "Deliver the Contraption",
                description: "\"Deliver this to Destinationville and do not delay!\" your supervisor had bellowed.  It was only your first day at the courier service, and it was clear no mistakes would be tolerated.  No worry, the town is a short walk away.",
                experience: 500,
                rewardId: "",
                sourceId: "messenger_dispatcher",
                sourceType: QuestSourceTypes.Character,
                sinkId: "stanley",
                sinkType: QuestSourceTypes.Character,
                itemIdsToDeliver: new(){"questitem_1"},
                quantitiesToDeliver: new() { 1 })
            }
        };

        public IList<IQuestStats> AvailableQuests { get; }

        public IList<IQuestStats> CompletedQuests { get; }

        public IQuestStats GetQuestById(string id)
        {
            if (!quests.ContainsKey(id)) return null;
            return quests[id];
        }
    }
}
