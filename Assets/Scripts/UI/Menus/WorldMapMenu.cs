using Data.Encounters;
using Data.Locations;
using InventoryQuest.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class WorldMapMenu : Menu
    {
        IGameStateDataSource _gameStateDataSource;
        IAdventureManager _adventureManager;
        IPathDataSource _pathDataSource;
        ILocationDataSource _locationDataSource;

        [SerializeField] List<MapLocationIcon> _mapLocations;
        [SerializeField] List<MapPathLine> _mapPathLines;

        [SerializeField] TextMeshProUGUI _currentLocationText;
        [SerializeField] TextMeshProUGUI _destinationLocationText;
        [SerializeField] TextMeshProUGUI _pathOverviewText;
        [SerializeField] PressAndHoldButton _continueButton;
        [SerializeField] PressAndHoldButton _returnButton;

        [Inject]
        public void Init(IGameStateDataSource gameStateDataSource, IAdventureManager adventureManager, IPathDataSource pathDataSource, ILocationDataSource locationDataSource)
        {
            _gameStateDataSource = gameStateDataSource;
            _adventureManager = adventureManager;
            _pathDataSource = pathDataSource;
            _locationDataSource = locationDataSource;
        }

        protected override void Awake()
        {
            base.Awake();
            foreach (var location in _mapLocations)
            {
                location.OnLocationSelected += OnLocationSelectedHandler;
            }
            _continueButton.OnPointerHoldSuccess += OnPathSelected;
            _returnButton.OnPointerHoldSuccess += OnReturnSelected;
        }

        private void OnReturnSelected(object sender, EventArgs e)
        {
            _adventureManager.Pathfinding.Return();
        }

        private void OnPathSelected(object sender, EventArgs e)
        {
            ChoosePath();
        }

        public override void Show()
        {
            base.Show();
            _continueButton.gameObject.SetActive(false);
            _currentLocationText.text = $"Current: {_gameStateDataSource.CurrentLocation?.Stats.DisplayName}";
            _destinationLocationText.text = "Destination: ...";
            var currentLocationID = _gameStateDataSource.CurrentLocation.Stats.Id;
            foreach (var location in _mapLocations)
            {
                if (location)
                {
                    ILocationStats stats = _locationDataSource.GetById(location.LocationId);
                    location.SetHighlight(location.LocationId == currentLocationID);
                    location.gameObject.SetActive(_gameStateDataSource.KnownLocations.Contains(stats.Id));
                }
            }
            foreach (var path in _mapPathLines)
            {
                path.HidePath();
            }
        }

        public override void Hide()
        {
            base.Hide();
        }


        bool debounce = false;
        void OnLocationSelectedHandler(object sender, string e)
        {
            if (debounce || _gameStateDataSource.CurrentLocation.Stats.Id == e) return;
            debounce = true;
            _pathOverviewText.text = "";
            _destinationLocationText.text = $"Destination: {e}";
            _gameStateDataSource.SetDestinationLocation(e);
            if (Debug.isDebugBuild)
                Debug.Log($"Destination selected : {e}");

            string currentId = _gameStateDataSource.CurrentLocation.Stats.Id;
            string destinationId = _gameStateDataSource.DestinationLocation.Stats.Id;
            foreach (var location in _mapLocations)
            {
                location.SetHighlight(location.LocationId == _gameStateDataSource.CurrentLocation.Stats.Id
                    || location.LocationId == _gameStateDataSource.DestinationLocation.Stats.Id);
            }

            foreach (var path in _mapPathLines)
            {
                if ((path.LocationAId == currentId || path.LocationBId == currentId)
                    &&(path.LocationAId == destinationId || path.LocationBId == destinationId))
                {
                    var stats = _pathDataSource.GetPathForStartAndEndLocations(
                        startLocationId: currentId,
                        endLocationId: destinationId);
                    if (stats == null) return;
                    _pathOverviewText.text = $"Projected Length: {stats.EncounterStats.Count} Encounters";
                    path.ActivatePath(stats);
                    _continueButton.gameObject.SetActive(true);
                }
                else
                {
                    path.HidePath();
                }
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
            _adventureManager.Pathfinding.Continue();
        }
    }
}