using Data;
using Data.Quests;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IQuestManager
    {
        List<IQuest> AvailableQuests { get; }
        List<IQuest> CompletedQuests { get; }
        List<IQuest> CurrentQuests { get; }

        event EventHandler<MessageEventArgs> OnQuestAccepted;
        event EventHandler<MessageEventArgs> OnQuestCanceled;
        event EventHandler<MessageEventArgs> OnQuestCompleted;

        void AddQuestToCurrentQuests(IQuest quest);
        void EvaluateCurrentQuests();
    }
}