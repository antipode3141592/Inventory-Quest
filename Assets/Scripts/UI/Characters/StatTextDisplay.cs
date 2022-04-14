using TMPro;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class StatTextDisplay : MonoBehaviour
    {
        [SerializeField]
        protected string statTypeName;

        public string StatTypeName => statTypeName;

        [SerializeField]
        protected TextMeshProUGUI labelText;
        [SerializeField]
        protected TextMeshProUGUI currentStatValueText;
        [SerializeField]
        protected TextMeshProUGUI statModifierText;
        protected void Awake()
        {
            HideModifier();
        }

        public virtual void UpdateText(string currentValue)
        {
            currentStatValueText.text = currentValue;
        }

        public virtual void ShowModifier(string modifier, bool isBuff = true)
        {
            statModifierText.text = modifier;
            Color textColor = isBuff ? Color.green : Color.red;
            statModifierText.color = textColor;
            labelText.color = textColor;
            currentStatValueText.color = textColor;
        }

        public virtual void HideModifier()
        {
            statModifierText.color = Color.clear;
            labelText.color = Color.white;
            currentStatValueText.color = Color.white;
        }
    }
}