using Data;
using Data.Locations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class MapLocationIcon: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        
        ILocationDataSource _locationDataSource;

        [SerializeField] protected string _locationId;
        [SerializeField] Image _icon;
        [SerializeField] Image _highlight;
        [SerializeField] TextMeshProUGUI _locationNameText;
        [SerializeField] ColorSettings _colorSettings;

        public event EventHandler<string> OnLocationSelected;

        public string LocationId => _locationId;

        [Inject]
        public void Init(ILocationDataSource locationDataSource)
        {
            _locationDataSource = locationDataSource;
        }

        void Awake()
        {
            _locationNameText.text = "";
            _highlight.color = Color.clear;
            var locationStats = _locationDataSource.GetById(_locationId);
            if (locationStats is null)
                return;
            _icon.sprite = locationStats.ThumbnailSprite;
            _locationNameText.text = locationStats.DisplayName;
        }

        public void SetHighlight(bool highlight)
        {
            _highlight.color = highlight ? _colorSettings.HighlightColor : Color.clear;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnLocationSelected?.Invoke(this, _locationId);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }
    }
}