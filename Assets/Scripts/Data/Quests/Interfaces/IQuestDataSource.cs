using System.Collections.Generic;

namespace Data.Quests
{
    public interface IQuestDataSource
    {
        public IList<IQuestStats> AvailableQuests { get; }
        public IList<IQuestStats> CompletedQuests { get; }

        public IQuestStats GetQuestById(string id);
    }
}
