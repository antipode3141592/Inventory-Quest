using Data;
using Data.Encounters;
using InventoryQuest.Managers;
using InventoryQuest.UI.Components;
using InventoryQuest.UI.Menus;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class EncounterDisplayController : MonoBehaviour, IOnMenuShow
    {
        IEncounterManager _encounterManager;
        IGameStateDataSource _gameStateDataSource;


        public TextMeshProUGUI NameText;
        public TextMeshProUGUI DescriptionText;

        [SerializeField] PressAndHoldButton continueButton;

        [SerializeField] EncounterSuccessDisplay encounterSuccessDisplay;
        [SerializeField] EncounterFailureDisplay encounterFailureDisplay;

        [SerializeField] RestEncounterDisplay restEncounterDisplay;
        [SerializeField] SkillCheckEncounterDisplay skillCheckEncounterDisplay;
        //[SerializeField] CombatEncounterDisplay combatEncounterDisplay;
        //[SerializeField] CraftingEncounterDisplay craftingEncounterDisplay;

        [Inject]
        public void Init(IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource)
        {
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
        }

        void Awake()
        {
            ClearDisplay();
        }

        void Start()
        {
            continueButton.OnPointerHoldSuccess += Continue;
            _encounterManager.Loading.OnEncounterLoaded += DisplayEncounter;
            _encounterManager.Resolving.OnEncounterResolveFailure += DisplayFailure;
            _encounterManager.Resolving.OnEncounterResolveSuccess += DisplaySuccess;
        }

        private void Continue(object sender, EventArgs e)
        {
            _encounterManager.Preparing.Continue();
        }

        void DisplaySuccess(object sender, string e)
        {
            encounterSuccessDisplay.gameObject.SetActive(true);
            encounterFailureDisplay.gameObject.SetActive(false);
            encounterSuccessDisplay.SuccessDescriptionText.text = _gameStateDataSource.CurrentEncounter.Stats.SuccessMessage;
        }

        void DisplayFailure(object sender, string e)
        {
            encounterFailureDisplay.gameObject.SetActive(true);
            encounterSuccessDisplay.gameObject.SetActive(false);
            encounterFailureDisplay.FailureDescriptionText.text = _gameStateDataSource.CurrentEncounter.Stats.FailureMessage;
        }

        void ClearDisplay()
        {
            Debug.Log($"ClearDisplay()", this);
            NameText.text = "";
            DescriptionText.text = "";

            skillCheckEncounterDisplay.gameObject.SetActive(false);
            restEncounterDisplay.gameObject.SetActive(false);
            //combatEncounterDisplay.gameObject.SetActive(false);
            //craftingEncounterDisplay.gameObject.SetActive(false);
            encounterSuccessDisplay.gameObject.SetActive(false);
            encounterFailureDisplay.gameObject.SetActive(false);
        }

        public void DisplayEncounter(object sender, string e)
        {
            ClearDisplay();
            encounterFailureDisplay.gameObject.SetActive(false);
            encounterSuccessDisplay.gameObject.SetActive(false);
            Debug.Log($"DisplayEncounter handling...", this);
            var encounter = _gameStateDataSource.CurrentEncounter;
            NameText.text = encounter.Stats.Name;
            DescriptionText.text = encounter.Stats.Description;
            Debug.Log($"Encounter: {encounter.Stats.Name}, {encounter.Stats.Description}, {encounter.Stats.Category}", this);
            //display encounter details
            SkillCheckEncounter skillEncounter = encounter as SkillCheckEncounter;
            if (skillEncounter is not null)
            {
                Debug.Log($"skill encounter", this);
                //enable skill check UI widget, SkillCheckEncounterDisplay
                skillCheckEncounterDisplay.gameObject.SetActive(true);
                skillCheckEncounterDisplay.SkillEncounter = skillEncounter;
                return;
            }
            CombatEncounter combatEncounter = encounter as CombatEncounter;
            if (combatEncounter is not null)
            {

                return;
            }
            CraftingEncounter craftingEncounter = encounter as CraftingEncounter;
            if (craftingEncounter is not null)
            {

                return;
            }
            RestEncounter restEncounter = encounter as RestEncounter;
            if (restEncounter is not null)
            {
                Debug.Log($"rest encounter", this);
                restEncounterDisplay.gameObject.SetActive(true);
                restEncounterDisplay.RestEncounter = restEncounter;
                return;
            }

        }

        public void OnShow()
        {
            
        }
    }
}