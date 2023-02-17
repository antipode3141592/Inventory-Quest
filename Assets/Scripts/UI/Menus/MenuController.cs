using InventoryQuest.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class MenuController : MonoBehaviour
    {
        IAdventureManager _adventureManager;
        IEncounterManager _encounterManager;
        IHarvestManager _harvestManager;
        IGameManager _gameManager;
        ISceneController _sceneController;
        IInputManager _inputManager;
        IContainerManager _containerManager;

        [SerializeField] List<Menu> _menuList;

        Type _currentMenuType;
        Type _previousMenuType;

        [SerializeField] LoadingScreen _loadingScreen;

        readonly Dictionary<Type, Menu> _menus = new();

        readonly Type _mainMenuKey = typeof(MainMenu);

        [Inject]
        public void Init(IAdventureManager adventureManager, IEncounterManager encounterManager, IHarvestManager harvestManager, IGameManager gameManager, ISceneController sceneController, IInputManager inputManager, IContainerManager containerManager)
        {
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
            _harvestManager = harvestManager;
            _gameManager = gameManager;
            _sceneController = sceneController;
            _inputManager = inputManager;
            _containerManager = containerManager;
        }

        void Awake()
        {
            foreach (var menu in _menuList)
            {
                _menus.Add(menu.GetType(), menu);
            }
            if (Debug.isDebugBuild)
            {
                Debug.Log($"_menus contains {_menus.Count} menus:");
                foreach (var menu in _menus)
                    Debug.Log($"{menu.Key.Name}");
            }
        }

        IEnumerator Start()
        {
            _gameManager.OnGameBeginning += OnGameBegining;
            _gameManager.OnGameOver += OnGameOver;
            _gameManager.OnGameRestart += OnGameRestart;

            _encounterManager.Wayfairing.StateEntered += OnWayfairingStart;
            _encounterManager.Resolving.StateEntered += OnResolvingStart;

            _adventureManager.InLocation.StateEntered += OnLocationEnteredHandler;
            _adventureManager.Pathfinding.StateEntered += OnPathfindingStartedHandler;
            _adventureManager.Adventuring.StateEntered += OnAdventureStartedHandler;
            //_adventureManager.Adventuring.StateExited += OnAdventureCompletedHandler;

            _harvestManager.Harvesting.StateEntered += OnHarvestingStartedHandler;
            _harvestManager.CleaningUpHarvest.StateEntered += OnHarvestCleaningUpStartedHandler;

            _sceneController.RequestShowLoading += ShowLoadingScreen;
            _sceneController.RequestHideLoading += HideLoadingScreen;

            _inputManager.OpenInventoryCommand += OpenInventoryScreen;
            _inputManager.CloseInventoryCommand += CloseInventoryScreen;

            _containerManager.OnContainersAvailable += OnContainersAvailablerHandler;

            yield return new WaitForSeconds(1f);
            _loadingScreen.FadeOff();
            OpenMenu(_mainMenuKey);
        }

        void OnLocationEnteredHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(LocationMenu));
        }

        void OnContainersAvailablerHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(InventoryMenu));
        }

        void OnGameRestart(object sender, EventArgs e)
        {
            OpenMenu(typeof(MainMenu));
        }

        void OnGameOver(object sender, EventArgs e)
        {
            Debug.Log($"PartyDeathHandler:   opening DeathMenu");
            OpenMenu(typeof(DeathMenu));
        }
        
        void CloseInventoryScreen(object sender, EventArgs e)
        {
            OpenPreviousMenu();
        }

        void OpenInventoryScreen(object sender, EventArgs e)
        {
            OpenMenu(typeof(InventoryMenu));
        }

        void HideLoadingScreen(object sender, EventArgs e)
        {
            _loadingScreen.FadeOff();
        }

        void ShowLoadingScreen(object sender, EventArgs e)
        {
            _loadingScreen.FadeOn();
        }

        void OnHarvestCleaningUpStartedHandler(object sender, EventArgs e)
        {
            OpenPreviousMenu();   
        }

        void OnHarvestingStartedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(HarvestMenu));
        }

        void OnResolvingStart(object sender, EventArgs e)
        {
            OpenMenu(typeof(TravelingMenu));
        }

        void OnGameBegining(object sender, EventArgs e)
        {
            OpenMenu(typeof(LocationMenu));
        }

        void OnWayfairingStart(object sender, EventArgs e)
        {
            OpenMenu(typeof(TravelingMenu));
        }

        void OnPathfindingStartedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(WorldMapMenu));
        }

        void OnAdventureStartedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(TravelingMenu));
        }

        void OpenMenu(Type menuType)
        {
            if (_currentMenuType is not null && menuType.Name == _currentMenuType.Name)
                return;
            if (Debug.isDebugBuild)
                Debug.Log($"OpenMenu({menuType.Name}) called");
            if (_currentMenuType == typeof(DeathMenu) && menuType != typeof(MainMenu))
                return;
            foreach (var menu in _menus)
            {
                if (menuType.Name == menu.Key.Name)
                {
                    menu.Value.Show();
                    _previousMenuType = _currentMenuType;
                    _currentMenuType = menuType;
                }
                else
                    menu.Value.Hide();
            }
        }

        void OpenPreviousMenu()
        {
            OpenMenu(_previousMenuType);
        }
    }
}
