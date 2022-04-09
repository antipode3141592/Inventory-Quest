using Data;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

namespace InventoryQuest.UI
{
    public class CharacterStatsDisplay : MonoBehaviour
    {
        EncounterManager _encounterManager;

        [SerializeField]
        List<StatTextDisplay> statTexts = new List<StatTextDisplay>();
        [SerializeField]
        CharacterCurrentMaxStatDisplay healthText;
        [SerializeField]
        CharacterCurrentMaxStatDisplay magicText;
        [SerializeField]
        CharacterCurrentMaxStatDisplay encumberanceText; 
        [SerializeField]
        CharacterCurrentMaxStatDisplay experienceText;
        [SerializeField]
        TextMeshProUGUI nameText;

        [SerializeField]
        TextMeshProUGUI speciesText; 

        Character _character;

        [Inject]
        public void Init(EncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        public void Awake()
        {
            _encounterManager.OnEncounterResolveSuccess += OnStatsUpdatedHandler;
            _encounterManager.OnEncounterResolveFailure += OnStatsUpdatedHandler;
        }

        public Character CurrentCharacter
        {
            get { return _character; }
            set
            {
                UnsubscribeToCharacterEvents();
                _character = value;
                SubcribeToCharacterEvents();
                UpdateStatBlock();
            }
        }

        void SubcribeToCharacterEvents()
        {
            if (_character == null) return;
            _character.OnStatsUpdated += OnStatsUpdatedHandler;
            nameText.text = _character.Stats.DisplayName;
            speciesText.text = _character.Stats.SpeciesId;
        }

        void UnsubscribeToCharacterEvents()
        {
            if (_character == null) return;

            _character.OnStatsUpdated -= OnStatsUpdatedHandler;
            nameText.text = "";
            speciesText.text = "";
        }

        void UpdateStatBlock()
        {

            foreach (var item in _character.Stats.Stats)
            {
                var stat = statTexts.Find(x => x.StatTypeName == item.Key.Name);
                if (stat is null) return;
                stat.UpdateText($"{item.Value.CurrentValue}");
            }

            healthText.UpdateText($"{_character.Stats.CurrentHealth}", $"{_character.Stats.MaximumHealth}");
            magicText.UpdateText($"{_character.Stats.CurrentMagicPool}", $"{_character.Stats.MaximumMagicPool}");
            encumberanceText.UpdateText($"{_character.CurrentEncumbrance}", $"{_character.Stats.MaximumEncumbrance}");
            experienceText.UpdateText($"{ _character.Stats.CurrentExperience}", $"{_character.Stats.NextLevelExperience}");
        }


        void OnStatsUpdatedHandler (object sender, EventArgs e)
        {
            UpdateStatBlock();
        }
    }
}