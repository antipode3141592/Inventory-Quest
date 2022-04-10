using Data.Quests;

namespace InventoryQuest.Quests
{
    internal class QuestFactory
    {
        public static IQuest GetQuest(IQuestStats questStats)
        {
            GatheringQuestStats gatheringStats = questStats as GatheringQuestStats;
            if (gatheringStats is not null) return new GatheringQuest(gatheringStats);
            return null;
        }
    }
}
