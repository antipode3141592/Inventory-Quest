using Data;
using Data.Characters;
using Data.Locations;
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
        IAdventureManager _adventureManager;
        IGameStateDataSource _gameStateDataSource;

        Party _party => _partyManager.CurrentParty;
        public List<IQuest> CurrentQuests { get; } = new();
        public List<IQuest> CompletedQuests { get; } = new();


        public event EventHandler<string> OnQuestAccepted;
        public event EventHandler<string> OnQuestCanceled;
        public event EventHandler<string> OnQuestCompleted;

        [Inject]
        public void Init(IGameManager gameManager, IPartyManager partyManager, IQuestDataSource questDataSource, IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource)
        {
            _gameManager = gameManager;
            _partyManager = partyManager;
            _questDataSource = questDataSource;
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
        }
        

        void Awake()
        {
            _gameStateDataSource.OnCurrentLocationSet += OnCurrentLocationSetHandler;

        }

        void Start()
        {
            var quest = QuestFactory.GetQuest(_questDataSource.GetQuestById("quest_intro_delivery"));
            CurrentQuests.Add(quest);
            OnQuestAccepted?.Invoke(this, quest.Id);
        }

        void OnCurrentLocationSetHandler(object sender, string e)
        {
            Debug.Log($"QuestManager handling Location Set...", this);
            //check current quests
            var quest = CurrentQuests.Find(x => x.Stats.SinkType == QuestSourceTypes.Location && x.Stats.SinkId == e);
            if (quest is null) return;
            if (quest.Evaluate(_party))
            {
                Debug.Log($"{quest.Name} is a success!");
            }
            //check for uncompleted quests that trigger on entering location
            //var newquest = _questDataSource.

        }

        public void EvaluateLocationCharacterQuests(string characterGuId)
        {
            var character = _gameStateDataSource.CurrentLocation.Characters.Find(x => x.GuId == characterGuId);
            Debug.Log($"QuestManager handling Character Guid{characterGuId} , Id {character.Stats.Id} Selected...", this);
            //check dialogue
            PixelCrushers.DialogueSystem.DialogueManager.StartConversation("Dispatch/Initial");

            //check current quests
            var quest = CurrentQuests.Find(x => x.Stats.SinkType == QuestSourceTypes.Character && x.Stats.SinkId == character.Stats.Id);
            if (quest is null) return;
            if (quest.Evaluate(_party))
            {
                Debug.Log($"{quest.Name} is a success!");
                CompletedQuests.Add(quest);
                CurrentQuests.Remove(quest);
                quest.Process(_party);
                foreach (var _character in _party.Characters)
                    _character.Value.CurrentExperience += quest.Stats.Experience;
                OnQuestCompleted?.Invoke(this, quest.Id);
            }
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
