using System;
using UnityEngine;
using Zenject;
using InventoryQuest;
using InventoryQuest.Managers;

namespace InventoryQuest.UI.Menus
{
    public class MainMenu : Menu
    {
        IAdventureManager _adventureManager;

        MenuController _menuController;
        [SerializeField] PressAndHoldButton continueButton;

        [Inject]

        public void Init(MenuController menuController, IAdventureManager adventureManager)
        {
            _menuController = menuController;
            _adventureManager = adventureManager;   
            
        }

        protected override void Awake()
        {
            base.Awake();
            continueButton.OnPointerHoldSuccess += OnContinueButtonHeld;
        }

        void OnContinueButtonHeld(object sender, EventArgs e)
        {

            Continue();
        }

        public void Continue()
        {
            _adventureManager.Idle.StartPath();
        }
    }
}
