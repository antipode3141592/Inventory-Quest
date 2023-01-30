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
        IEncounterManager _encounterManager;
        IGameStateDataSource _gameStateDataSource;
        IInputManager _inputManager;

        [SerializeField] PartyDisplay partyDisplay;
        [SerializeField] TextMeshProUGUI encounterText;

        [SerializeField] EncounterChoiceButton choiceButtonPrefab;
        [SerializeField] List<EncounterChoiceButton> choiceButtons = new();
        [SerializeField] RectTransform choicesGroup;
        [SerializeField] List<GameObject> encounterDisplayGroupItems;

        [SerializeField] Button InventoryButton;

        [Inject]
        public void Init(IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource, IInputManager inputManager)
        {
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
            _inputManager = inputManager;
        }

        protected override void Awake()
        {
            base.Awake();
            InventoryButton.onClick.AddListener(Inventory);
            
            foreach (var item in encounterDisplayGroupItems)
                item.SetActive(false);
        }

        void Start()
        {
            choiceButtons[0].ChoiceMade += OnChoiceMade;
            ToggleGroup(show: false);
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
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            _inputManager.OpenInventory();
        }

        void Retreat(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            base.Show();
            _encounterManager.Loading.OnEncounterLoaded += OnEncounterLoadedHandler;
            _encounterManager.Loading.StateExited += OnEncounterLoadingExitHandler;
        }

        public override void Hide()
        {
            base.Hide();
        }

        void OnEncounterLoadedHandler(object sender, string e)
        {
            StartCoroutine(SettingEncounterDisplay(_gameStateDataSource.CurrentEncounter));
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

            ToggleGroup(show: true);
            yield return null;
            encounterText.text = encounter.Stats.Description;
            for (int i = choiceButtons.Count; i < encounter.Stats.Choices.Count; i++)
            {
                choiceButtons.Add(Instantiate(choiceButtonPrefab, choicesGroup));
                choiceButtons[i].ChoiceMade += OnChoiceMade;
            }
            for (int i = 0; i < encounter.Stats.Choices.Count ; i++)
            {
                string rulesText = "";

                if (!choiceButtons[i].gameObject.activeInHierarchy)
                    choiceButtons[i].gameObject.SetActive(true);
                if (encounter.Stats.Choices[i] is SkillTest skillcheckreq)
                {
                    rulesText = skillcheckreq.ToString();
                        
                }
                else if (encounter.Stats.Choices[i] is GiveItems itemreq)
                {
                    rulesText = itemreq.ToString();
                }

                choiceButtons[i]
                    .ChangeButtonText(
                        descriptiveText: encounter.Stats.Choices[i].Description,
                        rulesText: rulesText)
                    .SetIndex(i);
            }
            for(int i = encounter.Stats.Choices.Count; i < choiceButtons.Count; i++)
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }

        void OnChoiceMade(object sender, int index)
        {
            _gameStateDataSource.CurrentEncounter.SetRequirement(_gameStateDataSource.CurrentEncounter.Stats.Choices[index]);
            Resolve();
        }
    }
};