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

        [SerializeField] MainMenu _mainMenu;
        [SerializeField] LocationMenu _locationMenu;
        [SerializeField] GameMenu _gameMenu;
        [SerializeField] AdventureMenu _adventureMenu;

        [SerializeField] LoadingScreen _loadingScreen;

        Dictionary<Type, Menu> _menus = new Dictionary<Type, Menu>();

        Type _mainMenuKey = typeof(MainMenu);

        [Inject]
        public void Init(IAdventureManager adventureManager)
        {
            _adventureManager = adventureManager;
        }

        private void Awake()
        {
            //***********************************************
            //TODO do this differently, oh god no
            _menus.Add(_mainMenu.GetType(), _mainMenu);
            _menus.Add(_locationMenu.GetType(), _locationMenu);
            _menus.Add(_gameMenu.GetType(), _gameMenu);
            _menus.Add(_adventureMenu.GetType(), _adventureMenu);

            
            //***********************************************

            _adventureManager.OnAdventureStarted += OnAdventureStartedHandler;
            _adventureManager.OnAdventureCompleted += OnAdventureCompletedHandler;
        }

        private void Start()
        {
            StartCoroutine(Initialize());
            
        }

        IEnumerator Initialize()
        {
            yield return new WaitForSeconds(1.5f);
            _loadingScreen.Fade();
            InitializeMenus();
        }

        private void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            _menus[typeof(MainMenu)].gameObject.SetActive(false);
            _menus[typeof(AdventureMenu)].gameObject.SetActive(false);
            _menus[typeof(GameMenu)].gameObject.SetActive(false);
            _menus[typeof(LocationMenu)].gameObject.SetActive(true);
        }

        private void OnAdventureStartedHandler(object sender, EventArgs e)
        {
            _menus[typeof(MainMenu)].gameObject.SetActive(false);
            _menus[typeof(AdventureMenu)].gameObject.SetActive(true);
            _menus[typeof(GameMenu)].gameObject.SetActive(false);
            _menus[typeof(LocationMenu)].gameObject.SetActive(false);
        }

        private void InitializeMenus()
        {
            _menus[typeof(MainMenu)].gameObject.SetActive(false);
            _menus[typeof(AdventureMenu)].gameObject.SetActive(false);
            _menus[typeof(GameMenu)].gameObject.SetActive(false);
            _menus[typeof(LocationMenu)].gameObject.SetActive(true);
        }

        public void OpenMenu(Type menuType)
        {
            foreach (var menu in _menus)
            {
                if (menuType == menu.GetType())
                {
                    menu.Value.gameObject.SetActive(true);
                } else
                {
                    menu.Value.gameObject.SetActive(true);
                }
            }
        }
    }

    public interface IMenu
    {

    }

    public abstract class Menu : MonoBehaviour, IMenu
    {

    }
}
