using Data.Characters;
using Data.Encounters;
using Data.Locations;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

namespace Data
{
    public class GameStateDataSource : SerializedMonoBehaviour, IGameStateDataSource
    {
        ICharacterDataSource _characterDataSource;
        ILocationDataSource _locationDataSource;
        IPathDataSource _pathDataSource;
        IEncounterDataSource _encounterDataSource;

        [SerializeField] ILocationStats startingLocation;

        [Inject]
        public void Init(ILocationDataSource locationDataSource, IPathDataSource pathDataSource, IEncounterDataSource encounterDataSource, ICharacterDataSource characterDataSource)
        {
            _locationDataSource = locationDataSource;
            _pathDataSource = pathDataSource;
            _encounterDataSource = encounterDataSource;
            _characterDataSource = characterDataSource;
        }

        public IPath CurrentPath { get; protected set; }
        public ILocation DestinationLocation { get; protected set; }
        public ILocation CurrentLocation { get; protected set; }
        public IEncounter CurrentEncounter { get; protected set; }

        public int CurrentIndex { get; set; }

        public event EventHandler<string> OnCurrentLocationSet;
        public event EventHandler<string> OnDestinationLocationSet;
        public event EventHandler<string> OnCurrentPathSet;

        void Start()
        {
            SetCurrentLocation(startingLocation.Id);
        }

        public void SetCurrentLocation(string id)
        {
            CurrentLocation = LocationFactory.GetLocation(_locationDataSource.GetById(id));
            CurrentLocation.InitializeLocation(_characterDataSource, _locationDataSource);
            
            OnCurrentLocationSet?.Invoke(this, $"{CurrentLocation.Stats.ScenePath}");
        }

        public void SetDestinationLocation(string id)
        {
            if (id == string.Empty)
            {
                DestinationLocation = null;
                return;
            }
            DestinationLocation = LocationFactory.GetLocation(_locationDataSource.GetById(id));
            OnDestinationLocationSet?.Invoke(this, id);
        }

        public void SetCurrentPath()
        {
            var stats = _pathDataSource.GetPathForStartAndEndLocations(
                startLocationId: CurrentLocation.Stats.Id,
                endLocationId: DestinationLocation.Stats.Id);
            if (stats == null) return;
            CurrentPath = PathFactory.GetPath(stats);
            CurrentIndex = 0;
            OnCurrentPathSet?.Invoke(this, stats.Id);
        }

        void LoadEncounter(string id)
        {
            if (id == string.Empty)
                CurrentEncounter = EncounterFactory.GetEncounter(_encounterDataSource.GetRandom());
            CurrentEncounter = EncounterFactory.GetEncounter(_encounterDataSource.GetById(id));
        }

        public void SetCurrentEncounter()
        {
            if (CurrentIndex >= CurrentPath.Length) return;
            string encounterId = CurrentPath.EncounterIds[CurrentIndex];
            LoadEncounter(encounterId);

        }
            //    SetCurrentLocation(DestinationLocation.Stats.Id);
            //    SetDestinationLocation("");
            //    EndAdventure = true;
    }
}
