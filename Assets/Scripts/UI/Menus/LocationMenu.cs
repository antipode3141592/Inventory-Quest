using InventoryQuest.Managers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class LocationMenu : Menu
    {
        IAdventureManager _adventureManager;
        IGameStateDataSource _gameStateDataSource;

        [SerializeField] TextMeshProUGUI locationName;
        [SerializeField] Image locationThumbnailIcon;
        [SerializeField] Image locationBackground;
        [SerializeField] PressAndHoldButton MainMapButton;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
        }

        protected override void Awake()
        {
            base.Awake();
            _gameStateDataSource.OnCurrentLocationSet += OnCurrentLocationLoadedHandler;
            MainMapButton.OnPointerHoldSuccess += OnLocationComplete;
        }

        void OnLocationComplete(object sender, EventArgs e)
        {
            _adventureManager.InLocation.Continue();
        }

        void OnCurrentLocationLoadedHandler(object sender, string e)
        {
            var stats = _gameStateDataSource.CurrentLocation.Stats;
            locationName.text = stats.DisplayName;
            locationThumbnailIcon.sprite = stats.ThumbnailSprite;
        }
    }
}