using InventoryQuest.Managers;
using InventoryQuest.Traveling;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Components
{
    public class AdventureEncounterMarker: MonoBehaviour
    {
        IEncounterManager _encounterManager;

        [SerializeField] Color fillColor;
        [SerializeField] Image fillBackground;
        [SerializeField] Image fillForeground;

        public Image AdventureIcon;
        public Image HighlightIcon;
        public string EncounterId;

        [Inject]
        public void Init(IEncounterManager encounterManager)
        {
            _encounterManager = encounterManager;
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
            _encounterManager.Wayfairing.TimerPercentComplete += TravelUpdateHandler;
        }

        

        public void UnsubscribeToTravel()
        {
            _encounterManager.Wayfairing.TimerPercentComplete -= TravelUpdateHandler;
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
