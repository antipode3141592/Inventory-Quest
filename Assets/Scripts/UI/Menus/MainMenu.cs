using System;
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

        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] TMP_InputField characterName;

        [SerializeField] PressAndHoldButton continueButton;

        [Inject]

        public void Init(IAdventureManager adventureManager, IPartyManager partyManager)
        {
            _adventureManager = adventureManager;
            _partyManager = partyManager;
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
            _partyManager.CurrentParty.SelectCharacter(_partyManager.CurrentParty.SelectedPartyMemberGuId).DisplayName = characterName.text;
            _adventureManager.Idle.Continue();
        }
    }
}
