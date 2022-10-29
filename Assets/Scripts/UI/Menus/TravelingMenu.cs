using Data;
using InventoryQuest.Managers;
using Zenject;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;
using Data.Encounters;

namespace InventoryQuest.UI.Menus
{
    public class TravelingMenu : Menu
    {
        IPartyManager _partyManager;
        IAdventureManager _adventureManager;
        IEncounterManager _encounterManager;
        IGameStateDataSource _gameStateDataSource;

        [SerializeField] PartyDisplay partyDisplay;

        [SerializeField] TextMeshProUGUI encounterText;
        [SerializeField] List<PressAndHoldButton> choiceButtons;
        [SerializeField] GameObject encounterDisplayGroup;

        [Inject]
        public void Init(IPartyManager partyManager, IAdventureManager adventureManager, IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource)
        {
            _partyManager = partyManager;
            _adventureManager = adventureManager;
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
        }


        protected override void Awake()
        {
            base.Awake();
            choiceButtons[0].gameObject.SetActive(false);
            //choiceButtons[0].OnPointerHoldSuccess += Retreat;
            choiceButtons[1].OnPointerHoldSuccess += Inventory;
            choiceButtons[2].OnPointerHoldSuccess += Resolve;
        }

        void Resolve(object sender, EventArgs e)
        {
            encounterDisplayGroup.SetActive(false);
            _encounterManager.Loading.IsLoaded = true;
        }

        void Inventory(object sender, EventArgs e)
        {
            _encounterManager.Loading.ManageInventory = true;
        }

        void Retreat(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            base.Show();
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

            IRestEncounterStats encounterCheck = encounter.Stats as IRestEncounterStats;
            if (encounterCheck is not null)
            {

            }

            ISkillCheckEncounterStats skillCheck = encounter.Stats as ISkillCheckEncounterStats;
            if (skillCheck is not null)
            {
                string choiceText = skillCheck.SkillCheckRequirements[0].Description;
                //choiceButtons[2].UpdateButtonText(choiceText);
            }

            choiceButtons[1].Select();
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