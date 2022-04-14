using InventoryQuest.Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Encounters
{
    public class EncounterStateDisplay : MonoBehaviour
    {
        EncounterManager _encounterManager;

        [SerializeField] List<SingleStateDisplay> displays = new List<SingleStateDisplay>();

        [Inject]
        public void Init(EncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        private void Awake()
        {
            _encounterManager.OnEncounterStateChanged += OnEncounterStateChangedHandler;
        }

        private void OnEncounterStateChangedHandler(object sender, EncounterStates e)
        {
            foreach(var statDisplay in displays)
            {
                statDisplay.SetHighlight(e);
            }
        }
    }
}
