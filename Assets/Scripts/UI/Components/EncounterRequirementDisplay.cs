using Data.Interfaces;
using InventoryQuest.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class EncounterRequirementDisplay : MonoBehaviour
    {
        GameManager _gameManager;
        EncounterManager _encounterManager;

        IEncounter currentEncounter;

        [SerializeField] protected TextMeshProUGUI requirementText;
        [SerializeField] protected Image statusImage;

        [Inject]
        public void Init(GameManager gameManager, EncounterManager encounterManager)
        {
            _gameManager = gameManager;
            _encounterManager = encounterManager;
        }

        private void Awake()
        {

        }

        private void OnEnable()
        {
            currentEncounter = _encounterManager.CurrentEncounter;
        }

        private void Start()
        {
            
        }
    }
}