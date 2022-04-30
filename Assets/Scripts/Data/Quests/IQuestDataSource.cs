namespace Data.Quests
{
    public interface IQuestDataSource
    {
        public IQuestStats GetQuestById(string id);
    }
}
