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
            _encounterManager.OnEncounterLoaded += DisplayEncounter;
            _encounterManager.OnEncounterResolveFailure += DisplayFailure;
            _encounterManager.OnEncounterResolveSuccess += DisplaySuccess;

        }

        void DisplaySuccess(object sender, EventArgs e)
        {
            ResultsImage.color = UIPreferences.TextBuffColor;
            encounterSuccessDisplay.gameObject.SetActive(true);
            encounterSuccessDisplay.SuccessDescriptionText.text = _encounterManager.CurrentEncounter.Stats.SuccessMessage;
        }

        void DisplayFailure(object sender, EventArgs e)
        {
            ResultsImage.color = UIPreferences.TextDeBuffColor;
            encounterFailureDisplay.gameObject.SetActive(true);
            encounterFailureDisplay.FailureDescriptionText.text = _encounterManager.CurrentEncounter.Stats.FailureMessage;
        }

        public void Continue()
        {
            ClearDisplay();
        }

        void ClearDisplay()
        {
            NameText.text = "";
            DescriptionText.text = "";
            EnounterTypeText.text = "";
            
            skillCheckEncounterDisplay.gameObject.SetActive(false);
            //combatEncounterDisplay.gameObject.SetActive(false);
            //craftingEncounterDisplay.gameObject.SetActive(false);
            encounterSuccessDisplay.gameObject.SetActive(false);
            encounterFailureDisplay.gameObject.SetActive(false);
        }

        public void DisplayEncounter(object sender, EventArgs e)
        {

            ResultsImage.color = Color.white;
            var encounter = _encounterManager.CurrentEncounter;
            NameText.text = encounter.Stats.Name;
            DescriptionText.text = encounter.Stats.Description;
            EnounterTypeText.text = encounter.Stats.Category;

            //display encounter details
            SkillCheckEncounter skillEncounter = encounter as SkillCheckEncounter;
            if (skillEncounter is not null)
            {
                
                //enable skill check UI widget, SkillCheckEncounterDisplay
                skillCheckEncounterDisplay.gameObject.SetActive(true);
                skillCheckEncounterDisplay.SkillEncounter = skillEncounter;
                

                skillCheckEncounterDisplay.DisplayRequirements();
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

        }
    }
}