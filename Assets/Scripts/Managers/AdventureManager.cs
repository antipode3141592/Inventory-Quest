using Data.Encounters;
using Data.Locations;
using System;
using UnityEngine;

using Zenject;

namespace InventoryQuest.Managers
{
    public class AdventureManager : MonoBehaviour, IAdventureManager
    {
        IEncounterManager _encounterManager;

        IEncounterDataSource _dataSource;
        IPathDataSource _pathDataSource;
        ILocationDataSource _locationDataSource;

        int currentIndex;

        public IPath CurrentPath { get; protected set; }
        public ILocation StartLocation { get; protected set; }
        public ILocation EndLocation { get; protected set; }

        public ILocation CurrentLocation { get; protected set; }


        public event EventHandler OnEncounterListGenerated;
        public event EventHandler OnAdventureStarted;
        public event EventHandler OnAdventureCompleted;
        public event EventHandler<AdventureStates> OnAdventureStateChanged;

        AdventureStates currentState;

        public AdventureStates CurrentState
        {
            get { return currentState; }
            set
            {
                OnAdventureStateChanged?.Invoke(this, value);
                Debug.Log($"current state: {value}");
                currentState = value;
            }
        }

        [Inject]
        public void Init(IEncounterDataSource dataSource, IPathDataSource pathDataSource, IEncounterManager encounterManager, ILocationDataSource locationDataSource)
        {
            _dataSource = dataSource;
            _pathDataSource = pathDataSource;
            _encounterManager = encounterManager;
            _locationDataSource = locationDataSource;
        }

        private void Awake()
        {
            currentState = AdventureStates.Idle;
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;

            
        }

        private void Start()
        {
            SetCurrentLocation("Startington");
        }

        public void SetCurrentLocation(string id)
        {
            CurrentLocation = LocationFactory.GetLocation(_locationDataSource.GetLocationById(id));
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

        private void OnEncounterCompleteHandler(object sender, string e)
        {
            Debug.Log($"{gameObject.name}: {currentState}, OnEncounterCompleteHandler()", this);
            //get next encounter in list
            if (currentIndex < CurrentPath.Length - 1)
            {
                currentIndex++;

                var nextEncounterId = CurrentPath.EncounterIds[currentIndex];
                //if it exists, start encounter
                Debug.Log($"load encounter: {nextEncounterId}");
                LoadEncounter(nextEncounterId);
                //else end adventure
            }
            else
            {
                currentState = AdventureStates.Idle;
                OnAdventureCompleted?.Invoke(this, EventArgs.Empty);
            }

        }


    }

    public enum AdventureStates { Idle, Pathfinding, Adventuring }
}