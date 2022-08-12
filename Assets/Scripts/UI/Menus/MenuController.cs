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

        [SerializeField] List<Menu> _menuList;

        [SerializeField] LoadingScreen _loadingScreen;

        Dictionary<Type, Menu> _menus = new();

        Type _mainMenuKey = typeof(MainMenu);

        [Inject]
        public void Init(IAdventureManager adventureManager)
        {
            _adventureManager = adventureManager;
        }

        private void Awake()
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

            _adventureManager.OnAdventureStarted += OnAdventureStartedHandler;
            _adventureManager.OnAdventureCompleted += OnAdventureCompletedHandler;
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(1.5f);
            _loadingScreen.Fade();
            OpenMenu(_mainMenuKey);
        }

        void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(LocationMenu));
        }

        void OnAdventureStartedHandler(object sender, EventArgs e)
        {
            OpenMenu(typeof(AdventureMenu));
        }



        public void OpenMenu(Type menuType)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"OpenMenu({menuType.Name}) called");
            foreach (var menu in _menus)
            {
                if (menuType.Name == menu.Key.Name)
                    menu.Value.Show();
                else
                    menu.Value.Hide();
            }
        }
    }
}
