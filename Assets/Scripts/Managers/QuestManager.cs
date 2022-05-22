using Data;
using Data.Characters;
using Data.Quests;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Data.Locations;

namespace InventoryQuest.Managers
{
    public class QuestManager : MonoBehaviour, IQuestManager
    {
        IGameManager _gameManager;
        IPartyManager _partyManager;
        IQuestDataSource _questDataSource;
        IAdventureManager _adventureManager;
        Party _party => _partyManager.CurrentParty;
        public List<IQuest> CurrentQuests { get; } = new();
        public List<IQuest> CompletedQuests { get; } = new();


        public event EventHandler<MessageEventArgs> OnQuestAccepted;
        public event EventHandler<MessageEventArgs> OnQuestCanceled;
        public event EventHandler<MessageEventArgs> OnQuestCompleted;

        [Inject]
        public void Init(IGameManager gameManager, IPartyManager partyManager, IQuestDataSource questDataSource, IAdventureManager adventureManager)
        {
            _gameManager = gameManager;
            _partyManager = partyManager;
            _questDataSource = questDataSource;
            _adventureManager = adventureManager;
        }

        private void Awake()
        {
            _adventureManager.OnCurrentLocationSet += OnCurrentLocationSetHandler;
        }

        private void OnCurrentLocationSetHandler(object sender, string e)
        {
            //check current quests
            var quest = CurrentQuests.Find(x => x.Stats.SinkType is ILocation && x.Stats.SinkId == e);
            if (quest is null) return;
            if (quest.Evaluate(_party))
            {
                Debug.Log($"{quest.Name} is a success!");
            }
            //check for uncompleted quests that trigger on entering location
            //var newquest = _questDataSource.

        }

        private void Start()
        {
            var quest = QuestFactory.GetQuest(_questDataSource.GetQuestById("quest_intro_delivery"));
            CurrentQuests.Add(quest);
            OnQuestAccepted?.Invoke(this, new MessageEventArgs(quest.Id));
        }

        public void EvaluateCurrentQuests()
        {
            foreach (var quest in CurrentQuests)
            {
                if (quest.Evaluate(_party))
                {

                }
            }
        }

        public void AddQuestToCurrentQuests(IQuest quest)
        {
            //CurrentQuests
        }
    }
}
