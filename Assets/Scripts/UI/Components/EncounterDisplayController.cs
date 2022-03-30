using Data.Encounters;
using InventoryQuest.Managers;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class EncounterDisplayController : MonoBehaviour
    {
        EncounterManager _encounterManager;
        PartyManager _partyManager;


        public TextMeshProUGUI NameText;
        public TextMeshProUGUI DescriptionText;

        [SerializeField] protected SkillCheckEncounterDisplay skillCheckEncounterDisplay;


        [Inject]
        public void Init(EncounterManager encounterManager, PartyManager partyManager)
        {
            _encounterManager = encounterManager;
            _partyManager = partyManager;
        }

        private void Awake()
        {
            _encounterManager.OnEncounterStart += DisplayEncounter;
        }

        public void DisplayEncounter(object sender, EventArgs e)
        {
            SkillCheckEncounter skillEncounter = _encounterManager.CurrentEncounter as SkillCheckEncounter;
            if (skillEncounter is not null)
            {
                //enable skill check UI widget, SkillCheckEncounterDisplay
                return;
            }
            CombatEncounter combatEncounter = _encounterManager.CurrentEncounter as CombatEncounter;
            if (combatEncounter is not null)
            {

                return;
            }
            CraftingEncounter craftingEncounter = _encounterManager.CurrentEncounter as CraftingEncounter;
            if (craftingEncounter is not null)
            {

                return;
            }

        }
    }
}