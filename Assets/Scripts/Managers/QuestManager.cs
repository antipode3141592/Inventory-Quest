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
        public void Init(IGameManager gameManager, IPartyManager partyManager)
        {
            _gameManager = gameManager;
            _partyManager = partyManager;

        }

        private void Awake()
        {
            availableQuests = new();
            currentQuests = new();
            completedQuests = new();
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
