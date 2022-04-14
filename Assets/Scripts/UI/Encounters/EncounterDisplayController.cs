using Data.Encounters;
using InventoryQuest.Managers;
using InventoryQuest.UI.Components;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class EncounterDisplayController : MonoBehaviour
    {
        EncounterManager _encounterManager;


        public TextMeshProUGUI NameText;
        public TextMeshProUGUI DescriptionText;
        public TextMeshProUGUI EnounterTypeText;
        public Image ResultsImage;

        [SerializeField] EncounterSuccessDisplay encounterSuccessDisplay;
        [SerializeField] EncounterFailureDisplay encounterFailureDisplay;

        [SerializeField] RestEncounterDisplay restEncounterDisplay;
        [SerializeField] SkillCheckEncounterDisplay skillCheckEncounterDisplay;
        //[SerializeField] CombatEncounterDisplay combatEncounterDisplay;
        //[SerializeField] CraftingEncounterDisplay craftingEncounterDisplay;

        [Inject]
        public void Init(EncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        void Awake()
        {
            ClearDisplay();
            

        }

        private void OnEnable()
        {
            _encounterManager.OnEncounterLoaded += DisplayEncounter;
            _encounterManager.OnEncounterResolveFailure += DisplayFailure;
            _encounterManager.OnEncounterResolveSuccess += DisplaySuccess;
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;
        }

        private void OnDisable()
        {
            _encounterManager.OnEncounterLoaded -= DisplayEncounter;
            _encounterManager.OnEncounterResolveFailure -= DisplayFailure;
            _encounterManager.OnEncounterResolveSuccess -= DisplaySuccess;
            _encounterManager.OnEncounterComplete -= OnEncounterCompleteHandler;
        }

        private void OnEncounterCompleteHandler(object sender, EventArgs e)
        {
            //ClearDisplay();
        }

        void DisplaySuccess(object sender, EventArgs e)
        {
            ResultsImage.color = UIPreferences.TextBuffColor;
            encounterSuccessDisplay.gameObject.SetActive(true);
            encounterFailureDisplay.gameObject.SetActive(false);
            encounterSuccessDisplay.SuccessDescriptionText.text = _encounterManager.CurrentEncounter.Stats.SuccessMessage;
        }

        void DisplayFailure(object sender, EventArgs e)
        {
            ResultsImage.color = UIPreferences.TextDeBuffColor;
            encounterFailureDisplay.gameObject.SetActive(true);
            encounterSuccessDisplay.gameObject.SetActive(false);
            encounterFailureDisplay.FailureDescriptionText.text = _encounterManager.CurrentEncounter.Stats.FailureMessage;
        }

        void ClearDisplay()
        {
            Debug.Log($"ClearDisplay()", this);
            NameText.text = "";
            DescriptionText.text = "";
            EnounterTypeText.text = "";

            skillCheckEncounterDisplay.gameObject.SetActive(false);
            restEncounterDisplay.gameObject.SetActive(false);
            //combatEncounterDisplay.gameObject.SetActive(false);
            //craftingEncounterDisplay.gameObject.SetActive(false);
            encounterSuccessDisplay.gameObject.SetActive(false);
            encounterFailureDisplay.gameObject.SetActive(false);
        }

        public void DisplayEncounter(object sender, EventArgs e)
        {
            ClearDisplay();
            encounterFailureDisplay.gameObject.SetActive(false);
            encounterSuccessDisplay.gameObject.SetActive(false);
            Debug.Log($"DisplayEncounter handling...", this);
            ResultsImage.color = Color.white;
            var encounter = _encounterManager.CurrentEncounter;
            NameText.text = encounter.Stats.Name;
            DescriptionText.text = encounter.Stats.Description;
            EnounterTypeText.text = encounter.Stats.Category;
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
    }
}