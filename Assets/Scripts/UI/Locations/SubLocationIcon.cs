using Data.Locations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Locations
{
    public class SubLocationIcon : SerializedMonoBehaviour, IEnableable
    {
        IGameStateDataSource _gameStateDataSource;

        [SerializeField] LocationStatsSO locationStats;
        [SerializeField] Image locationIconImage;
        [SerializeField] SpriteRenderer locationSpriteRenderer;
        [SerializeField] Image locationNameBackground;
        [SerializeField] TextMeshProUGUI locationNameText;

        bool isAvailable;
        public bool IsAvailable => isAvailable;

        bool isHidden;
        public bool IsHidden => isHidden;

        Color textColor;
        Color imageColor;
        Color textBackground;

        [Inject]
        public void Init(IGameStateDataSource gameStateDataSource)
        {
            _gameStateDataSource = gameStateDataSource;
        }

        void Awake()
        {
            textColor = locationNameText.color;
            imageColor = locationSpriteRenderer.color;
            textBackground = locationNameBackground.color;
        }

        void Start()
        {
            locationNameText.text = locationStats.DisplayName;
            locationIconImage.sprite = locationStats.ThumbnailSprite;
            if (locationStats.IsKnown)
                Show();
            else
                Hide();
        }

        public void OnMouseUpAsButton()
        {
            if (!isAvailable)
                return;
            if (Debug.isDebugBuild)
                Debug.Log($"PointerClick received, loading location {locationStats.DisplayName}", this);
            _gameStateDataSource.SetCurrentLocation(locationStats.Id);
        }

        void Hide()
        {
            locationNameText.color = Color.clear;
            locationIconImage.color = Color.clear;
            locationSpriteRenderer.color = Color.clear;
            locationNameBackground.color = Color.clear;
            isAvailable = false;
            isHidden = true;
        }

        void Show()
        {
            locationNameText.color = textColor;
            locationIconImage.color = Color.white;
            locationSpriteRenderer.color = imageColor;
            locationNameBackground.color = textBackground;
            isAvailable = true;
            isHidden = false;
        }

        public void Enable()
        {
            if (!IsHidden)
                isAvailable = true;
        }

        public void Disable()
        {
            if (!IsHidden)
                isAvailable = false;
        }
    }
}