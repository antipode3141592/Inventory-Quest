﻿using Data;
using Data.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class QuestManager: MonoBehaviour
    {
        GameManager _gameManager;
        PartyManager _partyManager;
        Party _party => _partyManager.CurrentParty;

        List<IQuest> availableQuests;
        List<IQuest> currentQuests;
        List<IQuest> completedQuests;

        [Inject]
        public void Init(GameManager gameManager, PartyManager partyManager)
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
            foreach(var quest in currentQuests)
            {
                if (quest.Evaluate(_party))
                { 

                }
            }
        }
    }
}