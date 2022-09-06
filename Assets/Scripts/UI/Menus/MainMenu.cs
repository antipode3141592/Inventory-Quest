using System;
using UnityEngine;
using Zenject;
using InventoryQuest;
using InventoryQuest.Managers;
using TMPro;

namespace InventoryQuest.UI.Menus
{
    public class MainMenu : Menu
    {
        IAdventureManager _adventureManager;

        [SerializeField] TextMeshProUGUI mainText;

        [SerializeField] PressAndHoldButton continueButton;

        [Inject]

        public void Init(IAdventureManager adventureManager)
        {
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
            _adventureManager.Idle.Continue();
        }
    }
}
