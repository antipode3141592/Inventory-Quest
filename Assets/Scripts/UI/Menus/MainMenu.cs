﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using InventoryQuest;
using InventoryQuest.Managers;
using TMPro;

namespace InventoryQuest.UI.Menus
{
    public class MainMenu : Menu
    {
        IAdventureManager _adventureManager;
        IPartyManager _partyManager;
        IGameManager _gameManager;

        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] TMP_InputField characterName;

        [SerializeField] PressAndHoldButton continueButton;

        [Inject]

        public void Init(IAdventureManager adventureManager, IPartyManager partyManager, IGameManager gameManager)
        {
            _adventureManager = adventureManager;
            _partyManager = partyManager;
            _gameManager = gameManager;
        }

        protected override void Awake()
        {
            base.Awake();
            continueButton.OnPointerHoldSuccess += OnContinueButtonHeld;
            
        }

        public override void Show()
        {
            base.Show();
            characterName.Select();
        }

        public override void Hide()
        {
            base.Hide();
        }

        void OnContinueButtonHeld(object sender, EventArgs e)
        {

            Continue();
        }



        public void Continue()
        {
            _partyManager.CurrentParty.SelectCharacter(_partyManager.CurrentParty.SelectedPartyMemberGuId).DisplayName = characterName.text;

            _gameManager.BeginGame();
        }
    }
}
