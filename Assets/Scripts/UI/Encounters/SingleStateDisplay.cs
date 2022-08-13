using InventoryQuest.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI.Encounters
{
    public class SingleStateDisplay : MonoBehaviour
    {
        [SerializeField] EncounterStates representedState;
        [SerializeField] Image backgroundImage;
        [SerializeField] TextMeshProUGUI stateNameText;
        [SerializeField] ColorSettings colorSettings;

        private void Awake()
        {
            stateNameText.text = representedState.ToString();
        }

        public void SetHighlight(EncounterStates currentState)
        {
            backgroundImage.color = currentState == representedState ? colorSettings.HighlightColor : Color.clear;
        }
    }
}
