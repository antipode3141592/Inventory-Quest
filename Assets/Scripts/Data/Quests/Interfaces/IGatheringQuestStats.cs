namespace Data.Quests
{
    public interface IGatheringQuestStats: IQuestStats
    {
        string TargetItemId { get; }
        int TargetQuantity { get; }
    }
}