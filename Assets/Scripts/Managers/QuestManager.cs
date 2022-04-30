using Data;
using Data.Characters;
using Data.Quests;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class QuestManager : MonoBehaviour, IQuestManager
    {
        IGameManager _gameManager;
        IPartyManager _partyManager;
        IQuestDataSource _questDataSource;
        Party _party => _partyManager.CurrentParty;

        List<IQuest> availableQuests;
        List<IQuest> currentQuests;
        List<IQuest> completedQuests;

        public List<IQuest> AvailableQuests => availableQuests;
        public List<IQuest> CurrentQuests => currentQuests;
        public List<IQuest> CompletedQuests => completedQuests;


        public event EventHandler<MessageEventArgs> OnQuestAccepted;
        public event EventHandler<MessageEventArgs> OnQuestCanceled;
        public event EventHandler<MessageEventArgs> OnQuestCompleted;

        [Inject]
        public void Init(IGameManager gameManager, IPartyManager partyManager, IQuestDataSource questDataSource)
        {
            _gameManager = gameManager;
            _partyManager = partyManager;
            _questDataSource = questDataSource;
        }

        private void Awake()
        {
            availableQuests = new();
            currentQuests = new();
            completedQuests = new();
        }

        private void Start()
        {
            var quest = QuestFactory.GetQuest(_questDataSource.GetQuestById("quest_intro_delivery"));
            CurrentQuests.Add(quest);
            OnQuestAccepted?.Invoke(this, new MessageEventArgs(quest.Id));
        }

        public void EvaluateCurrentQuests()
        {
            foreach (var quest in currentQuests)
            {
                if (quest.Evaluate(_party))
                {

                }
            }
        }

        public void AddQuestToCurrentQuests(IQuest quest)
        {
            availableQuests.Add(quest);
        }
    }
}
