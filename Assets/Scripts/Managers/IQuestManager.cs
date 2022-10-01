using Data;
using Data.Quests;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IQuestManager
    {
        List<IQuest> CompletedQuests { get; }
        List<IQuest> CurrentQuests { get; }

        event EventHandler<string> OnQuestAccepted;
        event EventHandler<string> OnQuestCanceled;
        event EventHandler<string> OnQuestCompleted;

        void AddQuestToCurrentQuests(IQuest quest);
        void EvaluateCurrentQuests();
        public void EvaluateLocationCharacterQuests(string characterGuId);

    }
}