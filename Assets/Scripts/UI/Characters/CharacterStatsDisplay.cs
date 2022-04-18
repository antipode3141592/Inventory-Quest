using Data.Characters;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class CharacterStatsDisplay : MonoBehaviour
    {
        IEncounterManager _encounterManager;

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

        PlayableCharacter _character;

        [Inject]
        public void Init(IEncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        public void Awake()
        {
            _encounterManager.OnEncounterResolveSuccess += OnStatsUpdatedHandler;
            _encounterManager.OnEncounterResolveFailure += OnStatsUpdatedHandler;
        }

        public PlayableCharacter CurrentCharacter
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
            if (_character is null) return;
            _character.OnStatsUpdated += OnCharacterStatsUpdatedHandler;
            nameText.text = _character.Stats.DisplayName;
            speciesText.text = _character.Stats.SpeciesId;
        }

        private void OnCharacterStatsUpdatedHandler(object sender, EventArgs e)
        {
            UpdateStatBlock();
        }

        void UnsubscribeToCharacterEvents()
        {
            if (_character is null) return;

            _character.OnStatsUpdated -= OnCharacterStatsUpdatedHandler;
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
            encumberanceText.UpdateText($"{_character.CurrentEncumbrance:0.#}", $"{_character.Stats.MaximumEncumbrance}");
            experienceText.UpdateText($"{ _character.Stats.CurrentExperience}", $"{_character.Stats.NextLevelExperience}");
        }


        void OnStatsUpdatedHandler (object sender, string e)
        {
            UpdateStatBlock();
        }
    }
}