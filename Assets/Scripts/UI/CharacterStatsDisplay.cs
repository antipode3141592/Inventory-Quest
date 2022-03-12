using Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class CharacterStatsDisplay : MonoBehaviour
    {
        [SerializeField]
        List<StatTextDisplay> statTexts = new List<StatTextDisplay>();
        [SerializeField]
        CharacterCurrentMaxStatDisplay healthText;
        [SerializeField]
        CharacterCurrentMaxStatDisplay encumberanceText;
        [SerializeField]
        CharacterCurrentMaxStatDisplay experienceText;

        Character _character;
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
        }

        void UnsubscribeToCharacterEvents()
        {
            if (_character == null) return;
            _character.OnStatsUpdated -= OnStatsUpdatedHandler;
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
            encumberanceText.UpdateText($"{_character.CurrentEncumbrance}", $"{_character.Stats.MaximumEncumbrance}");
            experienceText.UpdateText($"{ _character.Stats.CurrentExperience}", $"{_character.Stats.NextLevelExperience}");
        }


        void OnStatsUpdatedHandler (object sender, EventArgs e)
        {
            UpdateStatBlock();
        }
    }
}