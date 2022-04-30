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

        [SerializeField] TextMeshProUGUI pathNameText;
        [SerializeField] PressAndHoldButton startAdventureButton;
        [SerializeField] TextMeshProUGUI locationName;
        [SerializeField] Image locationThumbnailIcon;
        [SerializeField] Image locationBackground;

        [Inject]
        public void Init(IAdventureManager adventureManager, MenuController menuController)
        {
            _adventureManager = adventureManager;
            _menuController = menuController;
        }

        private void Awake()
        {
            startAdventureButton.OnPointerHoldSuccess += StartAdventure;
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