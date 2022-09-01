using Data;
using InventoryQuest.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class WorldMapMenu : Menu
    {
        IGameStateDataSource _gameStateDataSource;
        IAdventureManager _adventureManager;

        [SerializeField] List<MapLocationIcon> _mapLocations;

        [SerializeField] TextMeshProUGUI _currentLocationText;
        [SerializeField] TextMeshProUGUI _destinationLocationText;
        [SerializeField] TextMeshProUGUI _pathOverviewText;
        [SerializeField] PressAndHoldButton _pressAndHoldButton;

        [Inject]
        public void Init(IGameStateDataSource gameStateDataSource, IAdventureManager adventureManager)
        {
            _gameStateDataSource = gameStateDataSource;
            _adventureManager = adventureManager;
        }

        protected override void Awake()
        {
            base.Awake();
            foreach (var location in _mapLocations)
            {
                location.OnLocationSelected += OnLocationSelectedHandler;
            }
            _gameStateDataSource.SetCurrentLocation("Startington");

            _pressAndHoldButton.OnPointerHoldSuccess += OnPathSelected;
        }

        private void OnPathSelected(object sender, EventArgs e)
        {
            ChoosePath();
        }

        public override void Show()
        {
            base.Show();
            
            _currentLocationText.text = $"Current: {_gameStateDataSource.CurrentLocation?.Stats.DisplayName}";
            _destinationLocationText.text = "Destination: ...";
            var currentLocationID = _gameStateDataSource.CurrentLocation.Stats.Id;
            foreach (var location in _mapLocations)
                if (location)
                    location.SetHighlight(location.LocationId == currentLocationID);
        }

        public override void Hide()
        {
            base.Hide();
        }


        bool debounce = false;
        void OnLocationSelectedHandler(object sender, string e)
        {
            if (debounce) return;
            debounce = true;
            _destinationLocationText.text = $"Destination: {e}";
            _gameStateDataSource.SetDestinationLocation(e);
            if (Debug.isDebugBuild)
                Debug.Log($"Destination selected : {e}");
            foreach (var location in _mapLocations)
            {
                location.SetHighlight(location.LocationId == _gameStateDataSource.CurrentLocation.Stats.Id
                    || location.LocationId == _gameStateDataSource.DestinationLocation?.Stats.Id);
            }
            StartCoroutine(ResetDebounce());
        }

        IEnumerator ResetDebounce()
        {
            yield return new WaitForSeconds(0.2f);
            debounce = false;
        }


        public void ChoosePath()
        {
            _adventureManager.Pathfinding.ChoosePath();
        }
    }
}