using Data.Characters;
using Data.Encounters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace InventoryQuest.Managers
{
    public class EncounterManager: MonoBehaviour
    {
        PartyManager _partyManager;
        RewardManager _rewardManager;
        

        [SerializeField] 

        public EventHandler OnEncounterLoaded;
        public EventHandler OnEncounterStart;
        public EventHandler OnEncounterResolveStart;
        public EventHandler OnEncounterResolveSuccess;
        public EventHandler OnEncounterResolveFailure;
        public EventHandler OnEncounterComplete;

        bool isResolving = false;

        IEncounter currentEncounter;

        public IEncounter CurrentEncounter {
            get { return currentEncounter; }
            set {
                currentEncounter = value;
                OnEncounterLoaded?.Invoke(this, EventArgs.Empty);
            }
        }

        [Inject]
        public void Init(PartyManager partyManager, RewardManager rewardManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
        }


        //connected to UI Button
        public void ResolveCurrentEncounter()
        {
            if (isResolving) return;
            isResolving = true;
            
            StartCoroutine(EncounterResolveBegin());
        }

        IEnumerator EncounterResolveBegin()
        {
            OnEncounterResolveStart?.Invoke(this, EventArgs.Empty);
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
            StartCoroutine(EncounterCleanup());
        }

        IEnumerator EncounterCleanup()
        {
            Debug.Log("EncounterCleanup()");
            yield return new WaitForSeconds(1f);
            OnEncounterComplete?.Invoke(this, EventArgs.Empty);
        }

        void AwardExperience(IDictionary<string, PlayableCharacter> Characters)
        {
            foreach(var character in Characters)
            {
                character.Value.Stats.CurrentExperience += CurrentEncounter.Stats.Experience;
            }
        }

        void DistributePenalties(IDictionary<string, PlayableCharacter> Characters)
        {
            foreach (var character in Characters)
            {
                
            }
        }

        
    }
}
