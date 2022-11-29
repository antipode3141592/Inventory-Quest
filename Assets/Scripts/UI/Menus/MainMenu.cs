using InventoryQuest.Managers;
using PixelCrushers.DialogueSystem;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class MainMenu : Menu
    {
        IPartyManager _partyManager;
        IGameManager _gameManager;

        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] TMP_InputField characterName;

        [SerializeField] PressAndHoldButton continueButton;

        [Inject]

        public void Init(IPartyManager partyManager, IGameManager gameManager)
        {
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
            _partyManager.CurrentParty.SelectCharacter(_partyManager.CurrentParty.PartyDisplayOrder[0]).DisplayName = characterName.text;
            DialogueManager.ChangeActorName("Player", characterName.text);
            _gameManager.BeginGame();
        }
    }
}
