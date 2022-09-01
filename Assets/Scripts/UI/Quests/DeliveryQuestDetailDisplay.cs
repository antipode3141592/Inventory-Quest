using Data.Items;
using Data.Locations;
using Data.Quests;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI.Quests
{
    public class DeliveryQuestDetailDisplay : MonoBehaviour, IQuestDetailDisplay
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
            DeliveryQuestStats deliveryQuestStats = questStats as DeliveryQuestStats;
            if (deliveryQuestStats is null) return;
            foreach((string,int) item in deliveryQuestStats.DeliveryItemIdsAndQuantities)
            {
                var obj = Instantiate<DeliveryItemDisplay>(DeliveryItemPrefab, DeliveryItemsParentRect);
                IItemStats itemStats = _itemDataSource.GetItemStats(item.Item1);
                obj.SetItem(
                    itemSprite: Resources.Load<Sprite>(itemStats.SpritePath),
                    itemName: itemStats.Id,
                    quantity: item.Item2
                    );
                DeliveryItems.Add(obj);
            }

            ILocationStats locationStats = _locationDataSource.GetById(deliveryQuestStats.SinkId);
            Sprite locationSprite = Resources.Load<Sprite>(locationStats.ThumbnailSpritePath);
            DeliveryLocationImage.sprite = locationSprite;
            DeliveryLocationText.text = locationStats.DisplayName;
        }

        public void UpdateDisplay()
        {
            throw new System.NotImplementedException();
        }
    }
}
