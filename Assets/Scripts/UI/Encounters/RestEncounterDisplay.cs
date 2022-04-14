using Data.Encounters;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class RestEncounterDisplay : MonoBehaviour
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