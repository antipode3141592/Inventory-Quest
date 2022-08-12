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

        void OnCharacterStatsUpdatedHandler(object sender, EventArgs e)
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

            CharacterStats stats = _character.Stats;
            foreach (var charStat in stats.Stats)
            {
                var stat = statTexts.Find(x => x.StatTypeName == charStat.Key.Name);
                if (stat is null) return;
                stat.UpdateText(charStat.Value.CurrentValue);
            }

            healthText.UpdateText(stats.CurrentHealth, stats.MaximumHealth);
            magicText.UpdateText(stats.CurrentMagicPool, stats.MaximumMagicPool);
            encumberanceText.UpdateText(_character.CurrentEncumbrance, stats.MaximumEncumbrance);
            experienceText.UpdateText(stats.CurrentExperience, stats.NextLevelExperience);
        }


        void OnStatsUpdatedHandler (object sender, string e)
        {
            UpdateStatBlock();
        }
    }
}