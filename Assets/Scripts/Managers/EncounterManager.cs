using Data;
using Data.Encounters;
using Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace InventoryQuest.Managers
{
    public class EncounterManager: MonoBehaviour
    {
        PartyManager _partyManager;
        RewardManager _rewardManager;
        IEncounterDataSource _dataSource;

        [SerializeField] 

        public EventHandler OnEncounterCreated;
        public EventHandler OnEncounterStart;
        public EventHandler OnEncounterResolveStart;
        public EventHandler OnEncounterResolveSuccess;
        public EventHandler OnEncounterResolveFailure;
        public EventHandler OnEncounterComplete;

        bool isResolving = false;

        public IEncounter CurrentEncounter { get; set; }

        [Inject]
        public void Init(IEncounterDataSource dataSource, PartyManager partyManager, RewardManager rewardManager)
        {
            _dataSource = dataSource;
            _partyManager = partyManager;
            _rewardManager = rewardManager;
        }

        public void ResolveCurrentEncounter()
        {
            if (isResolving) return;
            isResolving = true;
            OnEncounterResolveStart?.Invoke(this, EventArgs.Empty);
            StartCoroutine(EncounterResolveBegin());
        }

        void LoadNextEncounter()
        {
            //cleanup
            StartCoroutine(EncounterCleanup());
        }

        IEnumerator EncounterResolveBegin()
        {
            yield return new WaitForSeconds(1f);
            if (CurrentEncounter.Resolve(_partyManager.CurrentParty))
            {
                Debug.Log($"The Encounter {CurrentEncounter.Name} is a success!");
                foreach (var reward in CurrentEncounter.RewardIds)
                {
                    Debug.Log($"Enqueuing {reward}");
                    _rewardManager.EnqueueReward(reward);
                }
                
                AwardExperience(_partyManager.CurrentParty.Characters);
                OnEncounterResolveSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log($"The Encounter {CurrentEncounter.Name} was a failure!");
                DistributePenalties(_partyManager.CurrentParty.Characters);
                OnEncounterResolveFailure?.Invoke(this, EventArgs.Empty);
            }
            isResolving = false;
            LoadNextEncounter();
        }

        IEnumerator EncounterCleanup()
        {
            Debug.Log("EncounterCleanup()");
            yield return new WaitForSeconds(1f);
            OnEncounterComplete?.Invoke(this, EventArgs.Empty);
        }

        void AwardExperience(IDictionary<string, Character> Characters)
        {
            foreach(var character in Characters)
            {
                character.Value.Stats.CurrentExperience += CurrentEncounter.Stats.Experience;
            }
        }

        void DistributePenalties(IDictionary<string, Character> Characters)
        {
            foreach (var character in Characters)
            {
                
            }
        }

        public void RetreatToSafety()
        {
            EndAdventure();
        }

        public void BeginAdventure()
        {
            Debug.Log("Begin new adventure!");
            GenerateEncounter();
            OnEncounterStart?.Invoke(this, EventArgs.Empty);
        }

        public void EndAdventure()
        {

        }

        public void GenerateEncounter()
        {
            CurrentEncounter = EncounterFactory.GetEncounter(_dataSource.GetRandomEncounter());
            OnEncounterCreated?.Invoke(this, EventArgs.Empty);
        }
    }

    //public interface IPath
    //{
    //    public string Id { get; }
    //    public string Name { get; }

    //    public string StartLocationId { get; }
    //    public string EndLocationId { get; }

    //    public int Length => EncounterIds.Count;

    //    public IList<string> EncounterIds { get; }
    //}



    //public interface ILocation
    //{
    //    public IList<IPath> Paths { get; }
    //}

    //public interface IMap
    //{
    //    public IList<ILocation> Locations { get; }
    //}
}
