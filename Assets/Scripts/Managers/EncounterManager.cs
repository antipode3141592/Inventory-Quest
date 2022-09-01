using Data.Characters;
using Data.Encounters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace InventoryQuest.Managers
{
    public class EncounterManager : MonoBehaviour, IEncounterManager
    {
        IPartyManager _partyManager;
        IRewardManager _rewardManager;

        public event EventHandler<string> OnEncounterLoaded;
        public event EventHandler<string> OnEncounterStart;
        public event EventHandler<string> OnEncounterResolveStart;
        public event EventHandler<string> OnEncounterResolveSuccess;
        public event EventHandler<string> OnEncounterResolveFailure;
        public event EventHandler<string> OnEncounterComplete;

        public event EventHandler<EncounterStates> OnEncounterStateChanged;

        bool isResolving = false;
        bool isEnding = false;

        IEncounter currentEncounter = null;
        EncounterStates currentState;
        EncounterStates CurrentState
        {
            get { return currentState; }
            set
            {
                Debug.Log($"EncounterManager current state: {value}", this);
                currentState = value;
                OnEncounterStateChanged?.Invoke(this, currentState);
            }
        }

        public IEncounter CurrentEncounter
        {
            get { return currentEncounter; }
            set
            {
                CurrentState = EncounterStates.Loading;
                currentEncounter = value;
                OnEncounterLoaded?.Invoke(this, value.Id);
            }
        }

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
        }

        void Awake()
        {
            CurrentState = EncounterStates.Idle;
        }

        public void ChangeState(EncounterStates targetState)
        {
            if (CurrentState == targetState) return;
            CurrentState = targetState;
        }


        //connected to UI Button
        public void Continue()
        {
            if (CurrentState == EncounterStates.Loading)
                ChangeState(EncounterStates.Preparing);
            else if (currentState == EncounterStates.Preparing)
                BeginResolution();
            else if (currentState == EncounterStates.Cleanup)
                EndEncounter();
        }


        void BeginResolution()
        {

            if (isResolving) return;
            isResolving = true;
            CurrentState = EncounterStates.Resolving;
            StartCoroutine(ResolveEncounterRoutine());
        }

        void BeginCleanup()
        {
            CurrentState = EncounterStates.Cleanup;
            _rewardManager.ProcessRewards();
        }

        void EndEncounter()
        {
            if (isEnding) return;
            isEnding = true;
            CurrentState = EncounterStates.End;
            _rewardManager.DestroyRewards();
            StartCoroutine(EndEncounterRoutine());
        }



        IEnumerator ResolveEncounterRoutine()
        {
            OnEncounterResolveStart?.Invoke(this, currentEncounter.Id);
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
                OnEncounterResolveSuccess?.Invoke(this, currentEncounter.Id);
            }
            else
            {
                Debug.Log($"The Encounter {CurrentEncounter.Name} was a failure!");
                DistributePenalties(_partyManager.CurrentParty.Characters);
                OnEncounterResolveFailure?.Invoke(this, currentEncounter.Id);
            }
            isResolving = false;
            BeginCleanup();
        }





        IEnumerator EndEncounterRoutine()
        {
            Debug.Log($"Ending encounter...", this);
            yield return new WaitForSeconds(1f);

            isEnding = false;
            CurrentState = EncounterStates.Idle;
            OnEncounterComplete?.Invoke(this, currentEncounter.Id);
        }

        void AwardExperience(IDictionary<string, PlayableCharacter> Characters)
        {
            foreach (var character in Characters)
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
