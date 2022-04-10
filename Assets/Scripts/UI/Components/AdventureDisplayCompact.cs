using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Components
{
    public class AdventureDisplayCompact: MonoBehaviour
    {
        AdventureManager _adventureManager;
        EncounterManager _encounterManager;


        [SerializeField] AdventureEncounterMarker encounterMarkerPrefab;
        List<AdventureEncounterMarker> adventureEncounterMarkers = new();

        [Inject]
        public void Init(AdventureManager adventureManager, EncounterManager encounterManager)
        {
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
        }

        private void Awake()
        {
            _encounterManager.OnEncounterLoaded += OnEncounterLoadedHandler;
            _encounterManager.OnEncounterResolveStart += OnEncounterResolveStartHandler;
            _encounterManager.OnEncounterResolveSuccess += OnEncounterResolveSuccessHandler;
            _encounterManager.OnEncounterResolveFailure += OnEncounterResolveFailureHandler;
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
            
        }
    }
}
