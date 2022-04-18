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

        [SerializeField] Transform markerDisplayParentTransform;


        [SerializeField] AdventureEncounterMarker encounterMarkerPrefab;
        List<AdventureEncounterMarker> adventureEncounterMarkers = new();

        [Inject]
        public void Init(IAdventureManager adventureManager, IEncounterManager encounterManager)
        {
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
        }

        private void Awake()
        {
            _adventureManager.OnEncounterListGenerated += OnEncounterListGeneratedHandler;
            _encounterManager.OnEncounterLoaded += OnEncounterLoadedHandler;
            _encounterManager.OnEncounterResolveStart += OnEncounterResolveStartHandler;
            _encounterManager.OnEncounterResolveSuccess += OnEncounterResolveSuccessHandler;
            _encounterManager.OnEncounterResolveFailure += OnEncounterResolveFailureHandler;
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;
            _adventureManager.OnAdventureCompleted += OnAdventureCompletedHandler;
        }

        private void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            for (int i = 0; i < adventureEncounterMarkers.Count; i++)
            {
                Destroy(adventureEncounterMarkers[i]);
            }
        }

        private void OnEncounterCompleteHandler(object sender, string e)
        {
            foreach(var marker in adventureEncounterMarkers)
            {
                marker.HighlightIcon.color = Color.clear;
            }
        }

        private void OnEncounterListGeneratedHandler(object sender, EventArgs e)
        {
            foreach (var encounterId in _adventureManager.CurrentPath.EncounterIds)
            {
                var go = Instantiate<AdventureEncounterMarker>(encounterMarkerPrefab, markerDisplayParentTransform);
                go.EncounterId = encounterId;
                adventureEncounterMarkers.Add(go);
            }
        }

        private void OnEncounterResolveFailureHandler(object sender, string e)
        {
            foreach (var marker in adventureEncounterMarkers)
            {
                if (marker.EncounterId == e)
                    marker.AdventureIcon.color = UIPreferences.TextDeBuffColor;
            }
        }

        private void OnEncounterResolveSuccessHandler(object sender, string e)
        {
            foreach (var marker in adventureEncounterMarkers)
            {
                if (marker.EncounterId == e)
                    marker.AdventureIcon.color = UIPreferences.TextBuffColor;
            }
        }

        private void OnEncounterResolveStartHandler(object sender, string e)
        {
            
        }

        private void OnEncounterLoadedHandler(object sender, string e)
        {
            foreach (var marker in adventureEncounterMarkers)
            {
                if (marker.EncounterId == e)
                    marker.HighlightIcon.color = UIPreferences.TextBuffColor;
            }
        }
    }
}
