using Data;
using InventoryQuest.Managers;
using Zenject;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace InventoryQuest.UI.Menus
{
    public class TravelingMenu : Menu
    {
        IPartyManager _partyManager;
        IAdventureManager _adventureManager;
        IEncounterManager _encounterManager;
        IGameStateDataSource _gameStateDataSource;

        PartyDisplay partyDisplay;

        [SerializeField] TextMeshProUGUI encounterText;
        [SerializeField] List<PressAndHoldButton> choiceButtons;
        [SerializeField] GameObject encounterDisplayGroup;

        [Inject]

        public void OnInit(IPartyManager partyManager, IAdventureManager adventureManager, IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource)
        {
            _partyManager = partyManager;
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
        }


        protected override void Awake()
        {
            base.Awake();
            partyDisplay = GetComponentInChildren<PartyDisplay>();
            choiceButtons[0].OnPointerHoldSuccess += Retreat;
            choiceButtons[1].OnPointerHoldSuccess += Inventory;
            choiceButtons[2].OnPointerHoldSuccess += Resolve;
        }

        private void Resolve(object sender, EventArgs e)
        {
            encounterDisplayGroup.SetActive(false);
            _encounterManager.Loading.IsLoaded = true;
        }

        private void Inventory(object sender, EventArgs e)
        {
            _encounterManager.Loading.ManageInventory = true;
        }

        private void Retreat(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            base.Show();
            partyDisplay.OnShow();
            _partyManager.CurrentParty.OnPartyMemberSelected += PartyMemberSelected;
            _encounterManager.Loading.OnEncounterLoaded += OnEncounterLoadedHandler;
            
            encounterDisplayGroup.SetActive(false);
        }

        void OnEncounterLoadedHandler(object sender, string e)
        {
            SetEncounterDisplay(e);
        }

        void SetEncounterDisplay(string encounterId)
        {
            encounterDisplayGroup.SetActive(true);
            
            var encounter = _gameStateDataSource.CurrentEncounter;       
            encounterText.text = encounter.Description;

        }

        public override void Hide()
        {
            base.Hide();
            _partyManager.CurrentParty.OnPartyMemberSelected -= PartyMemberSelected;
        }

        void PartyMemberSelected(object sender, string e)
        {
            
        }
    }
}