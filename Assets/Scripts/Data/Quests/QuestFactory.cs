namespace Data.Quests
{
    public class QuestFactory
    {
        public static Quest GetQuest(IQuestStats questStats)
        {
            IGatheringQuestStats gatheringStats = questStats as IGatheringQuestStats;
            if (gatheringStats is not null) return new GatheringQuest(gatheringStats);
            IDeliveryQuestStats deliveryStats = questStats as IDeliveryQuestStats;
            if (deliveryStats is not null) return new DeliveryQuest(deliveryStats);
            IBountyQuestStats bountyStats = questStats as IBountyQuestStats;
            if (bountyStats is not null) return new BountyQuest(bountyStats);
            IEscortQuestStats escortStats = questStats as IEscortQuestStats;
            if (escortStats is not null) return new EscortQuest(escortStats);
            return null;
        }
    }
}
