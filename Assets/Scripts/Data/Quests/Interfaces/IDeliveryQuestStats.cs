using System.Collections.Generic;

namespace Data.Quests
{
    public interface IDeliveryQuestStats: IQuestStats
    {
        public List<string> ItemIds { get; }
        public List<int> Quantities { get; }
    }
}