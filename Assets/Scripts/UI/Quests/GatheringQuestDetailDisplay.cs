using Data.Items;
using Data.Locations;
using Data.Quests;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace InventoryQuest.UI.Quests
{
    public class GatheringQuestDetailDisplay : MonoBehaviour, IQuestDetailDisplay
    {
        IItemDataSource _itemDataSource;
        ILocationDataSource _locationDataSource;

        [SerializeField] DeliveryItemDisplay DeliveryItemPrefab;
        [SerializeField] RectTransform DeliveryItemsParentRect;

        [SerializeField] Image DeliveryLocationImage;

        [SerializeField] TextMeshProUGUI DeliveryLocationText;

        List<DeliveryItemDisplay> DeliveryItems = new();

        public void Init(IItemDataSource itemDataSource, ILocationDataSource locationDataSource)
        {
            _itemDataSource = itemDataSource;
            _locationDataSource = locationDataSource;
        }

        public void SetDisplay(IQuestStats questStats)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateDisplay()
        {
            throw new System.NotImplementedException();
        }
    }
}
