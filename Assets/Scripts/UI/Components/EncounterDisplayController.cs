using Data.Encounters;
using InventoryQuest.Managers;
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
            _encounterManager.OnEncounterStart += DisplayEncounter;
            _encounterManager.OnEncounterResolveFailure += DisplayFailure;
            _encounterManager.OnEncounterResolveSuccess += DisplaySuccess;

        }

        void DisplaySuccess(object sender, EventArgs e)
        {
            ResultsImage.color = Color.green;
            ClearDisplay();
        }

        void DisplayFailure(object sender, EventArgs e)
        {
            ResultsImage.color = Color.red;
            ClearDisplay();
        }

        void ClearDisplay()
        {
            NameText.text = "";
            DescriptionText.text = "";
            EnounterTypeText.text = "";
            if (skillCheckEncounterDisplay.gameObject.activeInHierarchy)
                skillCheckEncounterDisplay.gameObject.SetActive(false);
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