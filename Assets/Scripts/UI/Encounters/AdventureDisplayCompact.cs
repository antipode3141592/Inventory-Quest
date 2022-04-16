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
        }

        private void OnEncounterCompleteHandler(object sender, EventArgs e)
        {
            foreach(var marker in adventureEncounterMarkers)
            {

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

        private void OnEncounterResolveFailureHandler(object sender, EventArgs e)
        {
            
        }

        private void OnEncounterResolveSuccessHandler(object sender, EventArgs e)
        {
            
        }

        private void OnEncounterResolveStartHandler(object sender, EventArgs e)
        {
            
        }

        private void OnEncounterLoadedHandler(object sender, EventArgs e)
        {
            //highlight current encounter
        }
    }
}
