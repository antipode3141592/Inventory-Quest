using InventoryQuest.Characters;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class CharacterStatsDisplay : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI textPrefab;

        List<TextMeshProUGUI> StatTexts = new List<TextMeshProUGUI>();

        Character _character;
        public Character CurrentCharacter
        {
            get { return _character; }
            set
            {
                _character = value;
                CreateStatBlock();
            }
        }

        void CreateStatBlock()
        {
            Debug.Log("Create Statblock...");
            int i = 0;
            foreach(var item in _character.Stats.Stats)
            {
                if (StatTexts.Count < _character.Stats.Stats.Count)
                {
                    var txt = Instantiate<TextMeshProUGUI>(textPrefab, gameObject.transform);
                    txt.gameObject.name = $"{item.Key.Name}StatText";
                    StatTexts.Add(txt);
                }
                StatTexts[i].text = $"{item.Key.Name}: {item.Value.CurrentValue} (initial: {item.Value.InitialValue})";
                i++;
            }
        }
    }
}