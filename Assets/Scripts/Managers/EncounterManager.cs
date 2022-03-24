using Data.Interfaces;
using System;
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

        public EventHandler OnEncounterResolveSuccess;
        public EventHandler OnEncounterResolveFailure;

        public IEncounter CurrentEncounter => currentEncounter;

        [Inject]
        public void Init(GameManager gameManager, IEncounterDataSource dataSource, PartyManager partyManager)
        {
            _gameManager = gameManager;
            _dataSource = dataSource;
            _partyManager = partyManager;
        }

        private void Awake()
        {
            
        }

        public void ResolveCurrentEncounter()
        {
            if (CurrentEncounter.Resolve(_partyManager.CurrentParty)) {
                foreach(var reward in CurrentEncounter.RewardIds)
                {
                    _rewardManager.ProcessReward(reward);
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
    }
}
