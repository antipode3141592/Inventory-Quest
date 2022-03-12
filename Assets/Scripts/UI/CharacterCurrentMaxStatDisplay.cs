using TMPro;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class CharacterCurrentMaxStatDisplay: StatTextDisplay
    {
        [SerializeField]
        protected TextMeshProUGUI currentStatMaxValueText;

        public void UpdateText(string currentValue, string maxValue)
        {
            currentStatValueText.text = currentValue;
            currentStatMaxValueText.text = maxValue;
        }

        public override void ShowModifier(string modifier, bool isBuff = true)
        {
            statModifierText.text = modifier;
            Color textColor = isBuff ? Color.green : Color.red;
            statModifierText.color = textColor;
            labelText.color = textColor;
            currentStatValueText.color = textColor;
        }

        public override void HideModifier()
        {
            statModifierText.color = Color.clear;
            labelText.color = Color.white;
            currentStatValueText.color = Color.white;
            currentStatMaxValueText.color = Color.white;
        }
    }
}
