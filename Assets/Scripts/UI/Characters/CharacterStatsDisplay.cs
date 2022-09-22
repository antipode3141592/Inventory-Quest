using Data.Characters;
using InventoryQuest.Managers;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class CharacterStatsDisplay : SerializedMonoBehaviour
    {
        IEncounterManager _encounterManager;

        [OdinSerialize] Dictionary<StatTypes, StatTextDisplay> statTexts;
        [SerializeField] CharacterCurrentMaxStatDisplay healthText;
        [SerializeField] CharacterCurrentMaxStatDisplay magicText;
        [SerializeField] CharacterCurrentMaxStatDisplay encumberanceText; 
        [SerializeField] CharacterCurrentMaxStatDisplay experienceText;
        [SerializeField] TextMeshProUGUI nameText;

        [SerializeField] TextMeshProUGUI speciesText; 

        ICharacter _character;

        [Inject]
        public void Init(IEncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
        }

        void Start()
        {
            _encounterManager.Resolving.OnEncounterResolveSuccess += OnStatsUpdatedHandler;
            _encounterManager.Resolving.OnEncounterResolveFailure += OnStatsUpdatedHandler;
        }

        public ICharacter CurrentCharacter
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
            nameText.text = _character.DisplayName;
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
            foreach (var charStat in _character.StatDictionary)
            {
                var stat = statTexts[charStat.Key];
                if (stat is null) return;
                stat.UpdateText(charStat.Value.CurrentValue);
            }
            healthText.UpdateText(_character.CurrentHealth, _character.MaximumHealth);
            magicText.UpdateText(_character.CurrentMagicPool, _character.MaximumMagicPool);
            encumberanceText.UpdateText(_character.CurrentEncumbrance, _character.MaximumEncumbrance);
            experienceText.UpdateText(_character.CurrentExperience, _character.NextLevelExperience);
        }


        void OnStatsUpdatedHandler (object sender, string e)
        {
            UpdateStatBlock();
        }
    }
}