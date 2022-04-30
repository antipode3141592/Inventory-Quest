namespace Data.Quests
{
    public class QuestFactory
    {
        public static Quest GetQuest(IQuestStats questStats)
        {
            GatheringQuestStats gatheringStats = questStats as GatheringQuestStats;
            if (gatheringStats is not null) return new GatheringQuest(gatheringStats);
            DeliveryQuestStats deliveryStats = questStats as DeliveryQuestStats;
            if (deliveryStats is not null) return new DeliveryQuest(deliveryStats);
            BountyQuestStats bountyStats = questStats as BountyQuestStats;
            if (bountyStats is not null) return new BountyQuest(bountyStats);
            EscortQuestStats escortStats = questStats as EscortQuestStats;
            if (escortStats is not null) return new EscortQuest(escortStats);
            return null;
        }
    }
}
