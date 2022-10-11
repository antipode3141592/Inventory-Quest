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

        [SerializeField] List<Menu> _menuList;

        Type _currentMenuType;
        Type _previousMenuType;

        [SerializeField] LoadingScreen _loadingScreen;

        Dictionary<Type, Menu> _menus = new();

        Type _mainMenuKey = typeof(MainMenu);

        [Inject]
        public void Init(IAdventureManager adventureManager, IEncounterManager encounterManager, IHarvestManager harvestManager, IGameManager gameManager)
        {
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
            _harvestManager = harvestManager;
            _gameManager = gameManager;

        }

        void Awake()
        {
            foreach(var menu in _menuList)
            {
                _menus.Add(menu.GetType(), menu);
            }
            if (Debug.isDebugBuild)
            {
                Debug.Log($"_menus contains {_menus.Count} menus:");
                foreach (var menu in _menus)
                    Debug.Log($"{menu.Key.Name}");
            }

            _gameManager.OnGameBegining += OnGameBegin;

            _encounterManager.Wayfairing.StateEntered += OnWayfairingStart;
            _encounterManager.ManagingInventory.StateEntered += OnManagingInventoryStart;
            _encounterManager.Resolving.StateEntered += OnResolvingStart;
            _encounterManager.CleaningUp.RequestShowInventory += OnCleaningUpShowInventory;

            _adventureManager.Pathfinding.StateEntered += OnPathfindingStartedHandler;
            _adventureManager.Adventuring.StateEntered += OnAdventureStartedHandler;
            _adventureManager.Adventuring.StateExited += OnAdventureCompletedHandler;

            _harvestManager.Harvesting.StateEntered += OnHarvestingStartedHandler;
            _harvestManager.CleaningUpHarvest.StateEntered += OnHarvestCleaningUpStartedHandler;
        }

        void OnHarvestCleaningUpStartedHandler(object sender, EventArgs e)
        {
            OpenPreviousMenu();   
        }

        void OnHarvestingStartedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(AdventureMenu));
        }

        void OnCleaningUpShowInventory(object sender, EventArgs e)
        {
            OpenMenu(typeof(AdventureMenu));
        }

        void OnResolvingStart(object sender, EventArgs e)
        {
            OpenMenu(typeof(TravelingMenu));
        }

        void OnGameBegin(object sender, EventArgs e)
        {
            OpenMenu(typeof(LocationMenu));
        }

        void OnManagingInventoryStart(object sender, EventArgs e)
        {
            OpenMenu(typeof(AdventureMenu));
        }

        void OnWayfairingStart(object sender, EventArgs e)
        {
            OpenMenu(typeof(TravelingMenu));
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            _loadingScreen.Fade();
            OpenMenu(_mainMenuKey);
        }

        void OnPathfindingStartedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(WorldMapMenu));
        }

        void OnAdventureStartedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(TravelingMenu));
        }

        void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(LocationMenu));
        }

        void OpenMenu(Type menuType)
        {
            if (_currentMenuType is not null && menuType.Name == _currentMenuType.Name)
                return;
            if (Debug.isDebugBuild)
                Debug.Log($"OpenMenu({menuType.Name}) called");
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
