﻿using Data.Characters;
using Data.Encounters;
using Data.Locations;
using InventoryQuest.Audio;
using InventoryQuest.Managers;
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
        IGameManager _gameManager;

        public IPathStats CurrentPathStats { get; protected set; }
        public ILocationStats DestinationLocation { get; protected set; }
        public ILocationStats CurrentLocation { get; protected set; }
        public IEncounter CurrentEncounter { get; protected set; }
        public ICollection<string> KnownLocations { get; } = new HashSet<string>();

        public int CurrentIndex { get; set; }

        public event EventHandler<string> OnCurrentEncounterSet;
        public event EventHandler<string> OnCurrentLocationSet;
        public event EventHandler<string> OnDestinationLocationSet;
        public event EventHandler<string> OnCurrentPathSet;

        [Inject]
        public void Init(ILocationDataSource locationDataSource, IPathDataSource pathDataSource, ICharacterDataSource characterDataSource, IGameManager gameManager)
        {
            _locationDataSource = locationDataSource;
            _pathDataSource = pathDataSource;
            _characterDataSource = characterDataSource;
            _gameManager = gameManager;
        }

        void Start()
        {
            Lua.RegisterFunction("RevealLocation", this, SymbolExtensions.GetMethodInfo(() => RevealLocation(string.Empty)));

            _gameManager.OnGameBeginning += GameBeginningHandler;
            _gameManager.OnGameOver += GameOverHandler;
        }

        void GameBeginningHandler(object sender, EventArgs e)
        {
            foreach (var location in _locationDataSource.Locations)
            {
                if (location.Value.IsKnown)
                    KnownLocations.Add(location.Value.Id);
            }
        }

        void GameOverHandler(object sender, EventArgs e)
        {
            KnownLocations.Clear();
        }

        public void SetCurrentLocation(string id)
        {
            CurrentLocation = _locationDataSource.GetById(id);
            if (Debug.isDebugBuild)
                Debug.Log($"SetCurrentLocation = {CurrentLocation.DisplayName} and path = {CurrentLocation.ScenePath}");
            OnCurrentLocationSet?.Invoke(this, $"{CurrentLocation.ScenePath}");
        }

        public void SetDestinationLocation(string id)
        {
            if (id == string.Empty)
            {
                DestinationLocation = null;
                return;
            }
            DestinationLocation = _locationDataSource.GetById(id);
            OnDestinationLocationSet?.Invoke(this, id);
        }

        public void SetCurrentPath()
        {
            Debug.Log($"SetCurrentPath()", this);
            var stats = _pathDataSource.GetPathForStartAndEndLocations(
                startLocationId: CurrentLocation.Id,
                endLocationId: DestinationLocation.Id);
            if (stats == null)
            {
                Debug.LogWarning($"path stats are blank!", this);
                return; 
            }
            CurrentPathStats = stats;
            CurrentIndex = 0;
            Debug.Log($"Path {CurrentPathStats.Id} set!", this);
            OnCurrentPathSet?.Invoke(this, stats.Id);
        }

        public void SetCurrentEncounter()
        {
            if (CurrentIndex >= CurrentPathStats.EncounterStats.Count) return;
            var encounterStats = CurrentPathStats.EncounterStats[CurrentIndex];
            Debug.Log($"encountStats id: {encounterStats.Id}", this);
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
