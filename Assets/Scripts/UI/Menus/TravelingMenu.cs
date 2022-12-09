using Data.Encounters;
using InventoryQuest.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class TravelingMenu : Menu
    {
        IPartyManager _partyManager;
        IEncounterManager _encounterManager;
        IGameStateDataSource _gameStateDataSource;

        [SerializeField] PartyDisplay partyDisplay;
        [SerializeField] TextMeshProUGUI encounterText;

        [SerializeField] EncounterChoiceButton choiceButtonPrefab;
        [SerializeField] List<EncounterChoiceButton> choiceButtons = new();
        [SerializeField] RectTransform choicesGroup;
        [SerializeField] List<GameObject> encounterDisplayGroupItems;

        [SerializeField] Button InventoryButton;

        [Inject]
        public void Init(IPartyManager partyManager, IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource)
        {
            _partyManager = partyManager;
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
        }

        protected override void Awake()
        {
            base.Awake();
            InventoryButton.onClick.AddListener(Inventory);
            choiceButtons[0].Button.onClick.AddListener(Resolve);
            foreach (var item in encounterDisplayGroupItems)
                item.SetActive(false);
        }

        void ToggleGroup(bool show)
        {
            foreach (var item in encounterDisplayGroupItems)
                item.SetActive(show);
        }

        void Resolve()
        {
            Debug.Log($"TravelingMenu.Resolve() called");
            ToggleGroup(show:false);
            _encounterManager.Loading.IsLoaded = true;
        }

        void Inventory()
        {
            Debug.Log($"TravelingMenu.Inventory() called");
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
            _encounterManager.Loading.StateExited += OnEncounterLoadingExitHandler;
            ToggleGroup(show: false);
        }

        public override void Hide()
        {
            base.Hide();
            _partyManager.CurrentParty.OnPartyMemberSelected -= PartyMemberSelected;
        }

        void PartyMemberSelected(object sender, string e)
        {

        }

        void OnEncounterLoadedHandler(object sender, string e)
        {
            var encounter = _gameStateDataSource.CurrentEncounter;

            StartCoroutine(SettingEncounterDisplay(encounter));
        }

        void OnEncounterLoadingExitHandler(object sender, EventArgs e)
        {
            ToggleGroup(show: false);
        }

        IEnumerator SettingEncounterDisplay(IEncounter encounter)
        {
            //empty and rest encounters auto resolve
            // and encounter display groups stay hidden
            IRestEncounterStats restEncounterStats = encounter.Stats as IRestEncounterStats;
            IEmptyEncounterStats emptyEncounterStats = encounter.Stats as IEmptyEncounterStats;
            if (emptyEncounterStats is not null || restEncounterStats is not null)
            {
                Debug.Log($"empty or rest encounter, autoresolving...");
                Resolve();
                yield break;
            }

            //skill checks show encounter text during Loading and Resolving states.  
            ISkillCheckEncounterStats skillCheck = encounter.Stats as ISkillCheckEncounterStats;
            if (skillCheck is not null)
            {
                Debug.Log($"skill check encounter, show encounter text and update choice button text");
                ToggleGroup(show: true);
                yield return null;
                encounterText.text = encounter.Description;
                choiceButtons[0].ChangeButtonText(skillCheck.SkillCheckRequirements[0].Description);
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            }
        }

        
    }
};