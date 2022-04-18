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

        [SerializeField] string selectedPath = "intro_path";

        [SerializeField] TextMeshProUGUI PathNameText;
        [SerializeField] PressAndHoldButton StartAdventureButton;

        [Inject]
        public void Init(IAdventureManager adventureManager, MenuController menuController)
        {
            _adventureManager = adventureManager;
            _menuController = menuController;
        }

        private void Awake()
        {
            StartAdventureButton.OnPointerHoldSuccess += StartAdventure;
            PathNameText.text = selectedPath;
        }

        private void StartAdventure(object sender, EventArgs e)
        {
            ChooseSelectedPath(selectedPath);
        }

        void ChooseSelectedPath(string selectedPath)
        {
            _menuController.OpenMenu(typeof(AdventureMenu));
            _adventureManager.ChoosePath(selectedPath);
        }
    }
}