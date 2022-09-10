namespace Data.Quests
{
    public interface IBountyQuestStats : IQuestStats
    {
        string BountyTargetId { get; }
        int BountyTargetQuantity { get; }
    }
}