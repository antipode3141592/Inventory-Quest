using Data;
using Data.Characters;
using FiniteStateMachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

namespace InventoryQuest.Managers.States
{
    public class Resolving : IState
    {
        IRewardManager _rewardManager;
        IPenaltyManager _penaltyManager;
        IPartyManager _partyManager;
        IGameStateDataSource _gameStateDataSource;

        public Resolving(IRewardManager rewardManager, IPenaltyManager penaltyManager, IPartyManager partyManager, IGameStateDataSource gameStateDataSource)
        {
            _rewardManager = rewardManager;
            _penaltyManager = penaltyManager;
            _partyManager = partyManager;
            _gameStateDataSource = gameStateDataSource;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public event EventHandler<float> OnEncounterResolveStart;

        public event EventHandler<string> OnEncounterResolveSuccess;
        public event EventHandler<string> OnEncounterResolveFailure;
        public event EventHandler<string> OnEncounterResolved;

        public bool EndState { get; private set; } = false;

        public bool IsDone { get; private set; } = false;
        

        public void OnEnter()
        {
            EndState = false;
            IsDone = false;
            BeginResolution();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        void BeginResolution()
        {
            if (EndState) return;
            EndState = true;
            float messageDuration = 0f;
            string _message;

            if (_gameStateDataSource.CurrentEncounter.Resolve(_partyManager.CurrentParty))
            {
                Debug.Log($"The Encounter {_gameStateDataSource.CurrentEncounter.Name} is a success!");
                if (_gameStateDataSource.CurrentEncounter.Rewards is not null)
                {
                    foreach (var reward in _gameStateDataSource.CurrentEncounter.Rewards)
                    {
                        Debug.Log($"Enqueuing {reward}");
                        _rewardManager.EnqueueReward(reward);
                    }
                }
                _message = _gameStateDataSource.CurrentEncounter.Stats.SuccessMessage;
                messageDuration = CalculateLength(_message.Length);

                AwardExperience(_partyManager.CurrentParty.Characters);
                OnEncounterResolveSuccess?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            }
            else
            {
                Debug.Log($"The Encounter {_gameStateDataSource.CurrentEncounter.Name} was a failure!");
                if (_gameStateDataSource.CurrentEncounter.Penalties is not null)
                {
                    foreach (var penalty in _gameStateDataSource.CurrentEncounter.Penalties)
                    {
                        Debug.Log($"Enqueing {penalty}");
                        _penaltyManager.EnqueuePenalty(penalty);
                    }
                }
                _message = _gameStateDataSource.CurrentEncounter.Stats.FailureMessage;
                messageDuration = CalculateLength(_message.Length);
                DistributePenalties(_partyManager.CurrentParty.Characters);
                OnEncounterResolveFailure?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            }
            DialogueManager.ShowAlert(_message, messageDuration);

            OnEncounterResolveStart?.Invoke(this, messageDuration);

            float CalculateLength(int messageLength) 
            {
                float calc = (float)messageLength / 27f;
                return calc > 2f ? calc : 2f ; 
            }
        }

        public void CompleteResolution()
        {
            OnEncounterResolved?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            IsDone = true;
        }



        void AwardExperience(IDictionary<string, ICharacter> Characters)
        {
            foreach (var character in Characters)
            {
                character.Value.CurrentExperience += _gameStateDataSource.CurrentEncounter.Stats.Experience;
            }
        }

        void DistributePenalties(IDictionary<string, ICharacter> Characters)
        {
            foreach (var character in Characters)
            {

            }
        }
    }
}