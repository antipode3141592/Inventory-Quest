using Data.Encounters;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace InventoryQuest.Managers
{
    public class EncounterManager: MonoBehaviour
    {
        GameManager _gameManager;
        PartyManager _partyManager;
        RewardManager _rewardManager;
        IEncounterDataSource _dataSource;

        IEncounter currentEncounter;

        public EventHandler OnEncounterListCreated;
        public EventHandler OnEncounterStart;
        public EventHandler OnEncounterResolveSuccess;
        public EventHandler OnEncounterResolveFailure;

        public IEncounter CurrentEncounter => currentEncounter;

        public IList<IEncounter> Encounters;

        [Inject]
        public void Init(GameManager gameManager, IEncounterDataSource dataSource, PartyManager partyManager, RewardManager rewardManager)
        {
            _gameManager = gameManager;
            _dataSource = dataSource;
            _partyManager = partyManager;
            _rewardManager = rewardManager;
        }

        private void Awake()
        {
            Encounters = new List<IEncounter>();
        }

        public void ResolveCurrentEncounter()
        {
            if (CurrentEncounter.Resolve(_partyManager.CurrentParty)) {
                foreach(var reward in CurrentEncounter.RewardIds)
                {
                    _rewardManager.EnqueueReward(reward);
                }
                AwardExperience();
                OnEncounterResolveSuccess?.Invoke(this, EventArgs.Empty);
            } 
            else
            {
                OnEncounterResolveFailure?.Invoke(this, EventArgs.Empty);
            }
        }

        void AwardExperience()
        {
            foreach(var character in _partyManager.CurrentParty.Characters)
            {
                character.Value.Stats.CurrentExperience += currentEncounter.Experience;
            }
        }

        public void RetreatToSafety()
        {

        }

        public void BeginAdventure()
        {

        }

        void GenerateEncounterList(int totalEncounters)
        {
            Encounters.Clear();
            for (int i = 0; i < totalEncounters; i++)
            {
                Encounters.Add(EncounterFactory.GetEncounter(_dataSource.GetRandomEncounter()));
            }
            OnEncounterListCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}
