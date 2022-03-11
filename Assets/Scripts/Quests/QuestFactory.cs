using Data;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
