using Data.Encounters;
using UnityEngine;

namespace InventoryQuest.UI.Components
{
    public class RestEncounterDisplay : EncounterDisplay
    {
        private RestEncounter restEncounter;

        public RestEncounter RestEncounter { 
            get => restEncounter;
            set
            {
                restEncounter = value;
            }
        }
    }
}