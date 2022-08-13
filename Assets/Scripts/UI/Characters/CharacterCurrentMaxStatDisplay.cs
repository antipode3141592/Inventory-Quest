using TMPro;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class CharacterCurrentMaxStatDisplay: StatTextDisplay
    {
        [SerializeField]
        protected TextMeshProUGUI currentStatMaxValueText;

        public void UpdateText(int currentValue, int maxValue)
        {
            currentStatValueText.text = $"{currentValue}";
            currentStatMaxValueText.text = $"{maxValue}";
        }
        public void UpdateText(float currentValue, float maxValue)
        {
            currentStatValueText.text = $"{currentValue:0.#}";
            currentStatMaxValueText.text = $"{maxValue:0.#}";
        }

        public override void ShowModifier(int modifier, bool isBuff = true)
        {
            statModifierText.text = $"{modifier:+#;-#;+0}";
            Color textColor = isBuff ? Color.green : Color.red;
            statModifierText.color = textColor;
            labelText.color = textColor;
            currentStatValueText.color = textColor;
            StartCoroutine(FadeOutModifier());
        }

        public void ShowModifier(float modifier, bool isBuff = true)
        {
            statModifierText.text = $"{modifier:+0.#;-0.#;+0}";
            Color textColor = isBuff ? Color.green : Color.red;
            statModifierText.color = textColor;
            labelText.color = textColor;
            currentStatValueText.color = textColor;
            StartCoroutine(FadeOutModifier());
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
