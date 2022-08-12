using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class MainMenu : Menu
    {
        MenuController _menuController;
        [SerializeField] PressAndHoldButton continueButton;

        [Inject]

        public void Init(MenuController menuController)
        {
            _menuController = menuController;
            
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
            _menuController.OpenMenu(typeof(LocationMenu));
        }
    }
}
