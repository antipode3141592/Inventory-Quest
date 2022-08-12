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

        [SerializeField] LocationIcon startingLocationIcon;
        [SerializeField] LocationIcon endingLocationIcon;

        [Inject]
        public void Init(IAdventureManager adventureManager, IEncounterManager encounterManager)
        {
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
        }

        void Awake()
        {
            _adventureManager.OnEncounterListGenerated += OnEncounterListGeneratedHandler;
            _encounterManager.OnEncounterLoaded += OnEncounterLoadedHandler;
            _encounterManager.OnEncounterResolveStart += OnEncounterResolveStartHandler;
            _encounterManager.OnEncounterResolveSuccess += OnEncounterResolveSuccessHandler;
            _encounterManager.OnEncounterResolveFailure += OnEncounterResolveFailureHandler;
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;
            _adventureManager.OnAdventureCompleted += OnAdventureCompletedHandler;
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

        void OnEncounterCompleteHandler(object sender, string e)
        {
        }

        void OnEncounterListGeneratedHandler(object sender, EventArgs e)
        {
            for (int i = 0; i < _adventureManager.CurrentPath.EncounterIds.Count; i++)
            {
                if (i >= adventureEncounterMarkers.Count)
                {
                    var go = Instantiate<AdventureEncounterMarker>(encounterMarkerPrefab, markerDisplayParentTransform);
                    adventureEncounterMarkers.Add(go);
                } else
                    adventureEncounterMarkers[i].gameObject.SetActive(true);
                adventureEncounterMarkers[i].EncounterId = _adventureManager.CurrentPath.EncounterIds[i];
                adventureEncounterMarkers[i].HighlightIcon.color = Color.clear;
                adventureEncounterMarkers[i].AdventureIcon.color = Color.gray;

            }

            startingLocationIcon.Set(Resources.Load<Sprite>(_adventureManager.CurrentLocation.Stats.ThumbnailSpritePath), _adventureManager.CurrentLocation.Stats.DisplayName);
            endingLocationIcon.Set(Resources.Load<Sprite>(_adventureManager.DestinationLocation.Stats.ThumbnailSpritePath), _adventureManager.DestinationLocation.Stats.DisplayName);
        }

        void OnEncounterResolveFailureHandler(object sender, string e)
        {
            foreach (var marker in adventureEncounterMarkers)
            {
                if (marker.EncounterId == e)
                    marker.AdventureIcon.color = UIPreferences.TextDeBuffColor;
            }
        }

        void OnEncounterResolveSuccessHandler(object sender, string e)
        {
            foreach (var marker in adventureEncounterMarkers)
            {
                if (marker.EncounterId == e)
                    marker.AdventureIcon.color = UIPreferences.TextBuffColor;
            }
        }

        void OnEncounterResolveStartHandler(object sender, string e)
        {
        }

        void OnEncounterLoadedHandler(object sender, string e)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"OnEncounterLoadedHandler for {e}");
            foreach (var marker in adventureEncounterMarkers)
            {
                if (marker.EncounterId == e)
                {
                    marker.HighlightIcon.color = Color.white;
                    if (Debug.isDebugBuild)
                        Debug.Log($"setting highlight for {marker.EncounterId}");
                }
                else
                {
                    marker.HighlightIcon.color = Color.clear;
                    if (Debug.isDebugBuild)
                        Debug.Log($"clearing highlight for {marker.EncounterId}");
                }
            }
        }
    }
}
