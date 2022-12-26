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
        readonly IRewardManager _rewardManager;
        readonly IPenaltyManager _penaltyManager;
        readonly IPartyManager _partyManager;
        readonly IGameStateDataSource _gameStateDataSource;
        readonly IDeltaTimeTracker _deltaTimeTracker;

        //timing

        bool enableTimer = false;
        float timer = 0f;
        float maxTime = 0f;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public event EventHandler<string> OnEncounterResolveSuccess;
        public event EventHandler<string> OnEncounterResolveFailure;
        public event EventHandler<string> OnEncounterResolved;

        public bool EndState { get; private set; } = false;
        public bool IsDone { get; private set; } = false;

        public Resolving(IRewardManager rewardManager, IPenaltyManager penaltyManager, IPartyManager partyManager, IGameStateDataSource gameStateDataSource, IDeltaTimeTracker deltaTimeTracker)
        {
            _rewardManager = rewardManager;
            _penaltyManager = penaltyManager;
            _partyManager = partyManager;
            _gameStateDataSource = gameStateDataSource;
            _deltaTimeTracker = deltaTimeTracker;
        }

        public void OnEnter()
        {
            EndState = false;
            IsDone = false;
            enableTimer = false;
            timer = 0f;
            BeginResolution();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
            RunTimer();
        }

        void BeginResolution()
        {
            if (EndState) return;
            EndState = true;
            float messageDuration;
            string _message = "";
            

            if (_gameStateDataSource.CurrentEncounter.Resolve(_partyManager.CurrentParty))
            {
                if (_gameStateDataSource.CurrentEncounter.ChosenChoice is not null)
                {
                    Debug.Log($"The Encounter {_gameStateDataSource.CurrentEncounter.Name} is a success!");
                    if (_gameStateDataSource.CurrentEncounter.ChosenChoice.Rewards is not null)
                    {
                        foreach (var reward in _gameStateDataSource.CurrentEncounter.ChosenChoice.Rewards)
                        {
                            Debug.Log($"Enqueuing {reward}");
                            _rewardManager.EnqueueReward(reward);
                        }
                    }
                    _message = _gameStateDataSource.CurrentEncounter.ChosenChoice.SuccessMessage;
                }
                
                messageDuration = CalculateLength(_message);

                AwardExperience(_partyManager.CurrentParty.Characters);
                OnEncounterResolveSuccess?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            }
            else
            {
                Debug.Log($"The Encounter {_gameStateDataSource.CurrentEncounter.Name} was a failure!");
                if (_gameStateDataSource.CurrentEncounter.ChosenChoice.Penalties is not null)
                {
                    foreach (var penalty in _gameStateDataSource.CurrentEncounter.ChosenChoice.Penalties)
                    {
                        Debug.Log($"Enqueing {penalty}");
                        _penaltyManager.EnqueuePenalty(penalty);
                    }
                }
                _message = _gameStateDataSource.CurrentEncounter.ChosenChoice.FailureMessage;
                messageDuration = CalculateLength(_message);
                _penaltyManager.ProcessPenalties();
                OnEncounterResolveFailure?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            }
            DialogueManager.ShowAlert(_message, messageDuration);
            maxTime = messageDuration;
            enableTimer = true;

            static float CalculateLength(string message) 
            {
                if (String.IsNullOrEmpty(message)) return 2f;
                float calc = (float)message.Length / 27f;
                return calc > 2f ? calc : 2f ; 
            }
        }

        public void CompleteResolution()
        {
            OnEncounterResolved?.Invoke(this, _gameStateDataSource.CurrentEncounter.Id);
            IsDone = true;
            enableTimer = false;
            timer = 0f;
        }

        void RunTimer()
        {
            if (enableTimer)
                timer += _deltaTimeTracker.DeltaTime;
            if (timer > maxTime  && EndState)
                CompleteResolution();
        }

        void AwardExperience(IDictionary<string, ICharacter> Characters)
        {
            foreach (var character in Characters)
            {
                character.Value.CurrentExperience += _gameStateDataSource.CurrentEncounter.ChosenChoice.Experience;
            }
        }
    }
}