using Data.Encounters;
using InventoryQuest.Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class SkillCheckEncounterDisplay : MonoBehaviour
    {
        public PartyManager _partyManager;

        public IList<EncounterRequirementDisplay> EncounterRequirements;
        public IList<EncounterRequirementDisplay> EncounterAlternateRequirements;

        public EncounterRequirementDisplay RequirementDisplayPrefab;

        public SkillCheckEncounter SkillEncounter { get; set; }

        [Inject]
        public void Init(PartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        void DisplayRequirements()
        {
            if (SkillEncounter is not null)
            {
                foreach (var req in SkillEncounter.SkillCheckRequirements)
                {
                    EncounterRequirements.Add(CreateRequirementElement(req));
                }
                foreach (var req in SkillEncounter.SkillCheckAlternates)
                {
                    EncounterAlternateRequirements.Add(CreateRequirementElement(req));
                }

            }
        }

        EncounterRequirementDisplay CreateRequirementElement(SkillCheckValue skillCheck)
        {
            EncounterRequirementDisplay display = Instantiate<EncounterRequirementDisplay>(RequirementDisplayPrefab, transform);
            display.RequirementText = skillCheck.ToString();
            display.SetStatusColor(SkillEncounter.Check(_partyManager.CurrentParty, skillCheck));
            return display;
        }
    }
}