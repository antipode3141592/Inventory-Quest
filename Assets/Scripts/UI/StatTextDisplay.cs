using TMPro;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class StatTextDisplay : MonoBehaviour
    {
        [SerializeField]
        string statTypeName;

        public string StatTypeName => statTypeName;

        [SerializeField]
        TextMeshProUGUI labelText;
        [SerializeField]
        TextMeshProUGUI currentStatValueText;
        [SerializeField]
        TextMeshProUGUI statModifierText;
        private void Awake()
        {
            HideModifier();
        }

        public void UpdateText(string currentValue)
        {
            currentStatValueText.text = currentValue;
        }

        public void ShowModifier(string modifier, bool isBuff = true)
        {
            statModifierText.text = modifier;
            Color textColor = isBuff ? Color.green : Color.red;
            statModifierText.color = textColor;
            labelText.color = textColor;
            currentStatValueText.color = textColor;
        }

        public void HideModifier()
        {
            statModifierText.color = Color.clear;
            labelText.color = Color.white;
            currentStatValueText.color = Color.white;
        }
    }
}