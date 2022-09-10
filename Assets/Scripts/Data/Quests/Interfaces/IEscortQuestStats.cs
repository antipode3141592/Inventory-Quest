namespace Data.Quests
{
    public interface IEscortQuestStats : IQuestStats
    {
        public string EscortCharacterId { get; }
        public string TargetLocationId { get; }
    }
}