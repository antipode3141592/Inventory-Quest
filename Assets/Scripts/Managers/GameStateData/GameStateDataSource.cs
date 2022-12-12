﻿using Data.Characters;
using Data.Encounters;
using Data.Locations;
using InventoryQuest.Audio;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest
{
    public class GameStateDataSource : SerializedMonoBehaviour, IGameStateDataSource
    {
        ICharacterDataSource _characterDataSource;
        ILocationDataSource _locationDataSource;
        IPathDataSource _pathDataSource;
        IAudioManager _audioManager;

        

        [Inject]
        public void Init(ILocationDataSource locationDataSource, IPathDataSource pathDataSource, ICharacterDataSource characterDataSource, IAudioManager audioManager)
        {
            _locationDataSource = locationDataSource;
            _pathDataSource = pathDataSource;
            _characterDataSource = characterDataSource;
            _audioManager = audioManager;
        }

        public IPathStats CurrentPathStats { get; protected set; }
        public ILocation DestinationLocation { get; protected set; }
        public ILocation CurrentLocation { get; protected set; }
        public IEncounter CurrentEncounter { get; protected set; }

        public ICollection<string> KnownLocations { get; } = new HashSet<string>();

        public int CurrentIndex { get; set; }

        public event EventHandler<string> OnCurrentEncounterSet;
        public event EventHandler<string> OnCurrentLocationSet;
        public event EventHandler<string> OnDestinationLocationSet;
        public event EventHandler<string> OnCurrentPathSet;

        void Start()
        {
            //SetCurrentLocation(startingLocation.Id);
            foreach(var location in _locationDataSource.Locations)
            {
                if (location.Value.IsKnown)
                    KnownLocations.Add(location.Value.Id);
            }

            Lua.RegisterFunction("RevealLocation", this, SymbolExtensions.GetMethodInfo(() => RevealLocation(string.Empty)));
        }

        public void SetCurrentLocation(string id)
        {
            CurrentLocation = LocationFactory.GetLocation(_locationDataSource.GetById(id));
            CurrentLocation.InitializeLocation(_characterDataSource, _locationDataSource);

            if (Debug.isDebugBuild)
                Debug.Log($"SetCurrentLocation = {CurrentLocation.Stats.DisplayName} and path = {CurrentLocation.Stats.ScenePath}");
            _audioManager.PlayMusicTrack(CurrentLocation.Stats.AudioClip);
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
            CurrentPathStats = stats;
            CurrentIndex = 0;
            OnCurrentPathSet?.Invoke(this, stats.Id);
        }

        public void SetCurrentEncounter()
        {
            if (CurrentIndex >= CurrentPathStats.EncounterStats.Count) return;
            var encounterStats = CurrentPathStats.EncounterStats[CurrentIndex];
            Debug.Log($"encountStats id: {encounterStats.Id}");
            CurrentEncounter = EncounterFactory.GetEncounter(encounterStats);
            OnCurrentEncounterSet?.Invoke(this, CurrentEncounter.Id);
        }
        
        public void RevealLocation(string locationId)
        {
            if (_locationDataSource.GetById(locationId) is not null)
            {
                KnownLocations.Add(locationId);
            }
        }
    }
}