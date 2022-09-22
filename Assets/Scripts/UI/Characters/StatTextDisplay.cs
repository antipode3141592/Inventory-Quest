using System.Collections;
using TMPro;
using UnityEngine;
using Data.Characters;
using Sirenix.OdinInspector;
using System;

namespace InventoryQuest.UI
{
    public class StatTextDisplay : MonoBehaviour
    {
        [SerializeField][EnumToggleButtons] protected StatTypes statType;
        [SerializeField] protected ColorSettings colorSettings;

        int previousValue = 0;

        public virtual StatTypes StatTypeName => statType;

        [SerializeField] protected TextMeshProUGUI labelText;
        [SerializeField] protected TextMeshProUGUI currentStatValueText;
        [SerializeField] protected TextMeshProUGUI statModifierText;

        [SerializeField, Range(0f, 1f)] float fadeOutTime = 0.7f;

        protected void Awake()
        {
            HideModifier();
        }

        public virtual void UpdateText(int currentValue)
        {
            currentStatValueText.text = $"{currentValue}";
            if (currentValue != previousValue) 
            {
                int diff = currentValue - previousValue;
                ShowModifier(diff, diff >= 0);
                previousValue = currentValue;
            }
        }

        public virtual void ShowModifier(int modifier, bool isBuff = true)
        {
            statModifierText.text = $"{modifier:+#;-#;+0}";
            Color textColor = isBuff ? colorSettings.BuffColor : colorSettings.DebuffColor;
            statModifierText.color = textColor;
            labelText.color = textColor;
            currentStatValueText.color = textColor;
            StartCoroutine(FadeOutModifier());
        }

        public virtual void HideModifier()
        {
            statModifierText.color = Color.clear;
            labelText.color = Color.white;
            currentStatValueText.color = Color.white;
        }

        protected IEnumerator FadeOutModifier()
        {
            yield return new WaitForSeconds(fadeOutTime);
            HideModifier();
        }
    }
}