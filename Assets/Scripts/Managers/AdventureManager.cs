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
        public ILocation DestinationLocation { get; protected set; }
        public ILocation CurrentLocation { get; protected set; }


        public event EventHandler OnEncounterListGenerated;
        public event EventHandler OnAdventureStarted;
        public event EventHandler OnAdventureCompleted;
        public event EventHandler<AdventureStates> OnAdventureStateChanged;
        public event EventHandler<string> OnCurrentLocationSet;
        public event EventHandler<string> OnDestinationLocationSet;


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
            SetDestinationLocation("Destinationville");
        }

        public void SetCurrentLocation(string id)
        {
            CurrentLocation = LocationFactory.GetLocation(_locationDataSource.GetLocationById(id));
            OnCurrentLocationSet?.Invoke(this, id);
        }

        public void SetDestinationLocation(string id)
        {
            DestinationLocation = LocationFactory.GetLocation(_locationDataSource.GetLocationById(id));
            OnDestinationLocationSet?.Invoke(this, id);
        }

        void StartPath(IPathStats pathStats)
        {

            currentState = AdventureStates.Pathfinding;
            //set current path 

            CurrentPath = PathFactory.GetPath(pathStats);


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
                string currentId = CurrentLocation.Stats.Id;
                SetCurrentLocation(DestinationLocation.Stats.Id);
                SetDestinationLocation(currentId);
                currentState = AdventureStates.Idle;
                OnAdventureCompleted?.Invoke(this, EventArgs.Empty);
            }

        }

        public void StartAdventure()
        {
            if (DestinationLocation is null) return;
            if (currentState == AdventureStates.Adventuring) return;
            var stats = _pathDataSource.GetPathForStartAndEndLocations(CurrentLocation.Stats.Id, DestinationLocation.Stats.Id);
            if (stats == null) return;
            StartPath(stats);
        }


    }
}