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
        MenuController _menuController;

        [SerializeField] TextMeshProUGUI pathNameText;
        [SerializeField] PressAndHoldButton startAdventureButton;
        [SerializeField] TextMeshProUGUI locationName;
        [SerializeField] Image locationThumbnailIcon;
        [SerializeField] Image locationBackground;
        [SerializeField] Image destinationLocationImage;
        [SerializeField] TextMeshProUGUI destinationLocationText;

        [Inject]
        public void Init(IAdventureManager adventureManager, MenuController menuController)
        {
            _adventureManager = adventureManager;
            _menuController = menuController;
        }

        private void Awake()
        {
            startAdventureButton.OnPointerHoldSuccess += StartAdventure;
            _adventureManager.OnCurrentLocationSet += OnCurrentLocationLoadedHandler;
            _adventureManager.OnDestinationLocationSet += OnDestinationSelectedHandler;
        }

        private void OnDestinationSelectedHandler(object sender, string e)
        {
            var stats = _adventureManager.DestinationLocation.Stats;
            destinationLocationText.text = stats.DisplayName;
            Sprite locationIcon = Resources.Load<Sprite>(stats.ThumbnailSpritePath);
            destinationLocationImage.sprite = locationIcon;
        }

        private void OnCurrentLocationLoadedHandler(object sender, string e)
        {
            var stats = _adventureManager.CurrentLocation.Stats;
            locationName.text = stats.DisplayName;
            Sprite locationIcon = Resources.Load<Sprite>(stats.ThumbnailSpritePath);
            locationThumbnailIcon.sprite = locationIcon;
        }

        private void StartAdventure(object sender, EventArgs e)
        {
            _menuController.OpenMenu(typeof(AdventureMenu));
            _adventureManager.StartAdventure();
            
        }
    }
}