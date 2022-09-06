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
            MainMapButton.OnPointerHoldSuccess += OnMainMapSelected;
        }

        void OnMainMapSelected(object sender, EventArgs e)
        {
            _adventureManager.Idle.Continue();
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

        public void OpenWorldMap()
        {

        }
    }
}