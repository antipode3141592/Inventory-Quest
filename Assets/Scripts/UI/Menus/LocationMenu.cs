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
        IInputManager _inputManager;

        [SerializeField] TextMeshProUGUI locationName;
        [SerializeField] Image locationThumbnailIcon;
        [SerializeField] Image locationBackground;
        [SerializeField] PressAndHoldButton MainMapButton;
        [SerializeField] Button InventoryButton;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource, IInputManager inputManager)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
            _inputManager = inputManager;
        }

        protected override void Awake()
        {
            base.Awake();
            InventoryButton.onClick.AddListener(Inventory);
            MainMapButton.OnPointerHoldSuccess += OnLocationComplete;
        }

        void Start()
        {
            _gameStateDataSource.OnCurrentLocationSet += OnCurrentLocationLoadedHandler;
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

        void Inventory()
        {
            Debug.Log($"TravelingMenu.Inventory() called");
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            _inputManager.OpenInventory();
        }
    }
}