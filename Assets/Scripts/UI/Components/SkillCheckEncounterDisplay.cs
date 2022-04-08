using Data.Encounters;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class SkillCheckEncounterDisplay : MonoBehaviour
    {
        PartyManager _partyManager;

        IList<EncounterRequirementDisplay> EncounterRequirements;

        public EncounterRequirementDisplay RequirementDisplayPrefab;

        public SkillCheckEncounter SkillEncounter { get; set; }

        [Inject]
        public void Init(PartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        private void Awake()
        {
            EncounterRequirements = new List<EncounterRequirementDisplay>();
            _partyManager.CurrentParty.OnPartyMemberStatsUpdated += UpdateRequirements;
        }

        public void DisplayRequirements()
        {
            if (SkillEncounter is not null)
            {
                for (int n = 0; n < EncounterRequirements.Count; n++)
                {
                    EncounterRequirements[n].gameObject.SetActive(false);
                }
                for (int i = 0; i < SkillEncounter.SkillCheckRequirements.Count; i++)
                {
                    var req = SkillEncounter.SkillCheckRequirements[i];
                    if (i >= EncounterRequirements.Count) 
                        EncounterRequirements.Add(CreateRequirementElement(req));
                    EncounterRequirements[i].gameObject.SetActive(true);
                    UpdateRequirement(EncounterRequirements[i], req);
                    
                }
            }
        }

        public void UpdateRequirements(object sender, EventArgs e)
        {
            Debug.Log($"UpdateRequirements() responding to {sender.GetType().Name}");
            if (SkillEncounter is null) return;
            for(int i = 0; i < SkillEncounter.SkillCheckRequirements.Count; i++)
            {
                UpdateRequirement(EncounterRequirements[i],SkillEncounter.SkillCheckRequirements[i]);
            }
        }

        void UpdateRequirement(EncounterRequirementDisplay display, SkillCheckRequirement skillCheck)
        {
            display.RequirementText = skillCheck.ToString();
            display.SetStatusColor(SkillEncounter.Check(_partyManager.CurrentParty, skillCheck));
        }

        EncounterRequirementDisplay CreateRequirementElement(SkillCheckRequirement skillCheck)
        {
            EncounterRequirementDisplay display = Instantiate<EncounterRequirementDisplay>(RequirementDisplayPrefab, transform);
            
            return display;
        }
    }
}