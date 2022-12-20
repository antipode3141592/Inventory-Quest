using InventoryQuest.Managers;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class DeathMenu : Menu
    {
        IGameManager _gameManager;

        [SerializeField] PressAndHoldButton returnToMainMenuButton;

        [Inject]
        public void Init(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }
        protected override void Awake()
        {
            base.Awake();
            returnToMainMenuButton.OnPointerHoldSuccess += ReturnToMainMenu;
        }

        void ReturnToMainMenu(object sender, EventArgs e)
        {
            _gameManager.MainMenuOpen();
        }
    }
}
