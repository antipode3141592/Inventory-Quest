using Data;
using InventoryQuest.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class TravelingMenu : Menu
    {
        IPartyManager _partyManager;
        IAdventureManager _adventureManager;
        IEncounterManager _encounterManager;

        PartyDisplay partyDisplay;

        [SerializeField] Button continueButton;

        [Inject]

        public void OnInit(IPartyManager partyManager, IAdventureManager adventureManager, IEncounterManager encounterManager)
        {
            _partyManager = partyManager;
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
        }


        protected override void Awake()
        {
            base.Awake();
            partyDisplay = GetComponentInChildren<PartyDisplay>();
            continueButton.onClick.AddListener(Continue);
        }

        public override void Show()
        {
            base.Show();
            partyDisplay.OnShow();
            _partyManager.CurrentParty.OnPartyMemberSelected += PartyMemberSelected;
        }

        public override void Hide()
        {
            base.Hide();
            _partyManager.CurrentParty.OnPartyMemberSelected -= PartyMemberSelected;
        }

        void PartyMemberSelected(object sender, MessageEventArgs e)
        {
            
        }

        public void Continue()
        {
            _encounterManager.Continue();
        }
    }
}