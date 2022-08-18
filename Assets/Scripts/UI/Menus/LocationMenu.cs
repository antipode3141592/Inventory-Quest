using Data;
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

        [SerializeField] TextMeshProUGUI pathNameText;
        [SerializeField] PressAndHoldButton startAdventureButton;
        [SerializeField] TextMeshProUGUI locationName;
        [SerializeField] Image locationThumbnailIcon;
        [SerializeField] Image locationBackground;
        [SerializeField] Image destinationLocationImage;
        [SerializeField] TextMeshProUGUI destinationLocationText;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
        }

        protected override void Awake()
        {
            base.Awake();
            startAdventureButton.OnPointerHoldSuccess += StartAdventure;
            _gameStateDataSource.OnCurrentLocationSet += OnCurrentLocationLoadedHandler;
            _gameStateDataSource.OnDestinationLocationSet += OnDestinationSelectedHandler;
        }

        void OnDestinationSelectedHandler(object sender, string e)
        {
            var stats = _gameStateDataSource.DestinationLocation.Stats;
            destinationLocationText.text = stats.DisplayName;
            Sprite locationIcon = Resources.Load<Sprite>(stats.ThumbnailSpritePath);
            destinationLocationImage.sprite = locationIcon;
        }

        void OnCurrentLocationLoadedHandler(object sender, string e)
        {
            var stats = _gameStateDataSource.CurrentLocation.Stats;
            locationName.text = stats.DisplayName;
            Sprite locationIcon = Resources.Load<Sprite>(stats.ThumbnailSpritePath);
            locationThumbnailIcon.sprite = locationIcon;
        }

        void StartAdventure(object sender, EventArgs e)
        {
            _adventureManager.Adventuring.StartAdventure();
        }
    }
}