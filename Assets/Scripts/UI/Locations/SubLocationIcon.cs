using Data.Locations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class SubLocationIcon : SerializedMonoBehaviour
    {
        IGameStateDataSource _gameStateDataSource;

        [SerializeField] LocationStatsSO locationStats;
        [SerializeField] Image locationImage;
        [SerializeField] TextMeshProUGUI locationNameText;

        [Inject]
        public void Init(IGameStateDataSource gameStateDataSource)
        {
            _gameStateDataSource = gameStateDataSource;
        }

        void Start()
        {
            locationNameText.text = locationStats.DisplayName;
            locationImage.sprite = locationStats.ThumbnailSprite;
        }

        public void OnMouseUpAsButton()
        {
            if (Debug.isDebugBuild)
                Debug.Log($"PointerClick received, loading location {locationStats.DisplayName}", this);
            _gameStateDataSource.SetCurrentLocation(locationStats.Id);
        }
    }
}