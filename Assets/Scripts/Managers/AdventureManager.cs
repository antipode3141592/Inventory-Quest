using Data.Encounters;
using System;
using UnityEngine;

using Zenject;

namespace InventoryQuest.Managers
{
    public class AdventureManager: MonoBehaviour
    {
        EncounterManager _encounterManager;
        RewardManager _rewardManager;

        IEncounterDataSource _dataSource;
        IPathDataSource _pathDataSource;

        int currentIndex;

        public IPath CurrentPath { get; set; }
        public ILocation StartLocation { get; set; }
        public ILocation EndLocation { get; set; }

        public EventHandler OnEncounterListGenerated;
        public EventHandler OnAdventureStarted;
        public EventHandler OnAdventureCompleted;
        public EventHandler<AdventureStates> OnAdventureStateChanged;

        AdventureStates currentState;

        public AdventureStates CurrentState
        {
            get { return currentState; }
            set
            {
                OnAdventureStateChanged?.Invoke(this,value);
                Debug.Log($"current state: {value}");
                currentState = value;
            }
        }

        [Inject]
        public void Init(IEncounterDataSource dataSource, IPathDataSource pathDataSource, EncounterManager encounterManager, RewardManager rewardManager)
        {
            _dataSource = dataSource;
            _pathDataSource = pathDataSource;
            _encounterManager = encounterManager;
            _rewardManager = rewardManager;
        }

        private void Awake()
        {
            currentState = AdventureStates.Idle;
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;
        }

        public void ChoosePath(string pathId)
        {

            currentState = AdventureStates.Pathfinding;
            //set current path 

            CurrentPath = PathFactory.GetPath(_pathDataSource.GetPathById(pathId));


            OnEncounterListGenerated?.Invoke(this, EventArgs.Empty);
            currentIndex = 0;
            LoadEncounter(CurrentPath.EncounterIds[currentIndex]);

        }

        void LoadEncounter(string id)
        {
            if (id == string.Empty)
                _encounterManager.CurrentEncounter = EncounterFactory.GetEncounter(_dataSource.GetRandomEncounter());
            _encounterManager.CurrentEncounter = EncounterFactory.GetEncounter(_dataSource.GetEncounterById(id));
            currentState = AdventureStates.Adventuring;
            OnAdventureStarted?.Invoke(this, EventArgs.Empty);
        }

        private void OnEncounterCompleteHandler(object sender, EventArgs e)
        {
            Debug.Log($"{gameObject.name}: {currentState}, OnEncounterCompleteHandler()", this);
            //get next encounter in list
            if (currentIndex < CurrentPath.Length-1)
            {
                currentIndex++;

                var nextEncounterId = CurrentPath.EncounterIds[currentIndex];
                //if it exists, start encounter
                Debug.Log($"load encounter: {nextEncounterId}");
                LoadEncounter(nextEncounterId);
                //else end adventure
            } else
            {
                currentState = AdventureStates.Idle;
                OnAdventureCompleted?.Invoke(this, EventArgs.Empty);
            }

        }

        
    }

    public enum AdventureStates { Idle, Pathfinding, Adventuring }
}