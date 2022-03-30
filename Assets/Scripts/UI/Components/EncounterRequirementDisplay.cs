using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class EncounterRequirementDisplay : MonoBehaviour
    {
        

        [SerializeField] protected TextMeshProUGUI requirementText;
        [SerializeField] protected Image statusImage;

        public string RequirementText 
        { 
            get { return requirementText.text; }
            set { requirementText.text = value; }
        }

        public void SetStatusColor(bool status)
        {
            statusImage.color = status ? Color.green : Color.red;
        }

    }
}