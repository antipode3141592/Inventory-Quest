using Data;
using Data.Items;
using Data.Locations;
using Data.Quests;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Quests
{
    public class QuestDisplayController : MonoBehaviour
    {
        IQuestManager _questManager;
        IItemDataSource _itemDataSource;
        ILocationDataSource _locationDataSource;

        IQuest selectedQuest;

        List<QuestDisplay> CurrentQuests { get; } = new();
        List<QuestDisplay> CompletedQuests { get; } = new();
        List<QuestDisplay> AvailableQuests { get; } = new();
        

        [SerializeField] QuestDisplay questDisplayPrefab;
        [SerializeField] RectTransform questLogParent;

        [Inject]
        public void Init(IQuestManager questManager, IItemDataSource itemDataSource, ILocationDataSource locationDataSource)
        {
            _questManager = questManager;
            _itemDataSource = itemDataSource;
            _locationDataSource = locationDataSource;
        }

        private void Awake()
        {
            _questManager.OnQuestAccepted += OnQuestAcceptedHandler;
        }

        private void OnQuestAcceptedHandler(object sender, MessageEventArgs e)
        {
            if (_questManager.CurrentQuests.Count == 0) return;
            foreach (var quest in _questManager.CurrentQuests)
            {
                QuestDisplay questDisplay = Instantiate<QuestDisplay>(questDisplayPrefab, questLogParent);
                questDisplay.Init(_itemDataSource, _locationDataSource);
                questDisplay.SetDisplay(quest);
                CurrentQuests.Add(questDisplay);
            }
        }

        public void AcceptQuest()
        {

        }

        public void CancelQuest()
        {

        }
    }
}
