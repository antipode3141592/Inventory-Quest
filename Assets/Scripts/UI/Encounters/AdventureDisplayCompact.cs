using Data;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Components
{
    public class AdventureDisplayCompact: MonoBehaviour
    {
        IAdventureManager _adventureManager;
        IEncounterManager _encounterManager;
        IGameStateDataSource _gameStateDataSource;

        [SerializeField] Transform markerDisplayParentTransform;

        [SerializeField] AdventureEncounterMarker encounterMarkerPrefab;
        readonly List<AdventureEncounterMarker> adventureEncounterMarkers = new();

        [SerializeField] LocationIcon startingLocationIcon;
        [SerializeField] LocationIcon endingLocationIcon;

        [Inject]
        public void Init(IAdventureManager adventureManager, IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource)
        {
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
        }

        void Start()
        {
            _encounterManager.Wayfairing.StateEntered += OnWayfairingEnteredHandler;
            _encounterManager.Resolving.StateEntered += OnResolvingStarted;
            _encounterManager.Loading.OnEncounterLoaded += OnEncounterLoadedHandler;
            _encounterManager.Resolving.OnEncounterResolveSuccess += OnEncounterResolveSuccessHandler;
            _encounterManager.Resolving.OnEncounterResolveFailure += OnEncounterResolveFailureHandler;

            _adventureManager.Adventuring.StateEntered += OnEncounterListGeneratedHandler;
            _adventureManager.Adventuring.StateExited += OnAdventureCompletedHandler;
        }

        void OnWayfairingEnteredHandler(object sender, EventArgs e)
        {
            for (int i = 0; i < adventureEncounterMarkers.Count; i++)
            {
                if (i == _gameStateDataSource.CurrentIndex)
                    adventureEncounterMarkers[i].SubscribeToTravel();
            }
        }

        void OnResolvingStarted(object sender, EventArgs e)
        {
            for(int i = 0; i < adventureEncounterMarkers.Count; i++)
            {
                if (i == _gameStateDataSource.CurrentIndex)
                    adventureEncounterMarkers[i].UnsubscribeToTravel(); 
            }
        }

        void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"setting {adventureEncounterMarkers.Count} adventure markers inactive...");
            for (int i = 0; i < adventureEncounterMarkers.Count; i++)
            {
                adventureEncounterMarkers[i].gameObject.SetActive(false);
            }
        }

        void OnEncounterListGeneratedHandler(object sender, EventArgs e)
        {
            for (int i = 0; i < _gameStateDataSource.CurrentPathStats.EncounterStats.Count; i++)
            {
                if (i >= adventureEncounterMarkers.Count)
                {
                    var go = Instantiate<AdventureEncounterMarker>(encounterMarkerPrefab, markerDisplayParentTransform);
                    go.Init(encounterManager: _encounterManager);
                    adventureEncounterMarkers.Add(go);
                } 
                else
                    adventureEncounterMarkers[i].gameObject.SetActive(true);

                adventureEncounterMarkers[i].EncounterId = _gameStateDataSource.CurrentPathStats.EncounterStats[i].Id;
                adventureEncounterMarkers[i].HighlightIcon.color = Color.clear;
                adventureEncounterMarkers[i].AdventureIcon.color = Color.gray;

            }

            startingLocationIcon.Set(_gameStateDataSource.CurrentLocation.ThumbnailSprite, _gameStateDataSource.CurrentLocation.DisplayName);
            endingLocationIcon.Set(_gameStateDataSource.DestinationLocation.ThumbnailSprite, _gameStateDataSource.DestinationLocation.DisplayName);
        }

        void OnEncounterResolveFailureHandler(object sender, string e)
        {
            for (int i = 0; i < adventureEncounterMarkers.Count; i++)
            {
                if (i == _gameStateDataSource.CurrentIndex)
                    adventureEncounterMarkers[i].AdventureIcon.color = UIPreferences.TextDeBuffColor;
            }
        }

        void OnEncounterResolveSuccessHandler(object sender, string e)
        {
            for (int i = 0; i < adventureEncounterMarkers.Count; i++)
            {
                if (i == _gameStateDataSource.CurrentIndex)
                    adventureEncounterMarkers[i].AdventureIcon.color = UIPreferences.TextBuffColor;
            }
        }

        void OnEncounterLoadedHandler(object sender, string e)
        {
            for (int i = 0; i < adventureEncounterMarkers.Count; i++)
            {
                if (i == _gameStateDataSource.CurrentIndex)
                    adventureEncounterMarkers[i].HighlightIcon.color = Color.white;
                else
                    adventureEncounterMarkers[i].HighlightIcon.color = Color.clear;
            }
        }
    }
}
