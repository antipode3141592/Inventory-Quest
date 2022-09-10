using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;

namespace Data.Quests
{
    public class QuestDataSourceSOTest: SerializedMonoBehaviour, IQuestDataSource
    {
        [OdinSerialize]
        Dictionary<string, QuestStatsSO> quests;

        public IList<IQuestStats> AvailableQuests { get; }

        public IList<IQuestStats> CompletedQuests { get; }

        public IQuestStats GetQuestById(string id)
        {
            if (!quests.ContainsKey(id)) return null;
            return quests[id] as IQuestStats;
        }
    }
}
