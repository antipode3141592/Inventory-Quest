using InventoryQuest.Traveling;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI.Components
{
    public class AdventureEncounterMarker: MonoBehaviour
    {
        [SerializeField] Color fillColor;
        [SerializeField] Image fillBackground;
        [SerializeField] Image fillForeground;

        TravelingPartyController travelingPartyController;

        public Image AdventureIcon;
        public Image HighlightIcon;
        public string EncounterId;

        void Awake()
        {
            travelingPartyController = FindObjectOfType<TravelingPartyController>();
        }

        void OnEnable()
        {
            ResetIcons();
        }

        void ResetIcons()
        {
            HighlightIcon.color = Color.clear;
            fillForeground.rectTransform.localScale = new Vector3(0, 1, 1);
        }

        public void SubscribeToTravel()
        {
            travelingPartyController.TravelPercentageUpdate += TravelUpdateHandler;
        }

        

        public void UnsubscribeToTravel()
        {
            travelingPartyController.TravelPercentageUpdate -= TravelUpdateHandler;
            fillForeground.rectTransform.localScale = new Vector3(1,1,1);
        }

        void TravelUpdateHandler(object sender, float e)
        {
            SetFill(e);
        }

        void SetFill(float fillPercentage)
        {
            fillForeground.rectTransform.localScale = new Vector3(
                x: Mathf.Clamp01(fillPercentage), 
                y: 1f, 
                z: 1f);
        }
    }
}
