using Data.Characters;
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
        ICharacterDataSource _characterDataSource;

        [SerializeField] DeliveryItemDisplay DeliveryItemPrefab;
        [SerializeField] RectTransform DeliveryItemsParentRect;
        
        [SerializeField] Image DeliveryLocationImage;
        
        [SerializeField] TextMeshProUGUI DeliveryLocationText;

        List<DeliveryItemDisplay> DeliveryItems = new();
        
        public void Init(IItemDataSource itemDataSource, ILocationDataSource locationDataSource, ICharacterDataSource characterDataSource)
        {
            _itemDataSource = itemDataSource;
            _locationDataSource = locationDataSource;
            _characterDataSource = characterDataSource;
        }

        public void SetDisplay(IQuestStats questStats)
        {
            IDeliveryQuestStats deliveryQuestStats = questStats as IDeliveryQuestStats;
            if (deliveryQuestStats is null) return;
            for(int i = 0; i < deliveryQuestStats.ItemIds.Count; i++)
            {
                var obj = Instantiate<DeliveryItemDisplay>(DeliveryItemPrefab, DeliveryItemsParentRect);
                IItemStats itemStats = _itemDataSource.GetItemStats(deliveryQuestStats.ItemIds[i]);
                obj.SetItem(
                    itemSprite: Resources.Load<Sprite>(itemStats.SpritePath),
                    itemName: itemStats.Id,
                    quantity: deliveryQuestStats.Quantities[i]
                    );
                DeliveryItems.Add(obj);
            }

            Sprite sprite = null;
            string displayName = "";
            if (deliveryQuestStats.SinkType == QuestSourceTypes.Location)
            {
                ILocationStats locationStats = _locationDataSource.GetById(deliveryQuestStats.SinkId);
                displayName = locationStats.DisplayName;
                sprite = Resources.Load<Sprite>(locationStats.ThumbnailSpritePath);
            }
            if (deliveryQuestStats.SinkType == QuestSourceTypes.Character)
            {
                ICharacterStats characterStats = _characterDataSource.GetCharacterStats(deliveryQuestStats.SinkId);
                displayName = characterStats.DisplayName;
                sprite = Resources.Load<Sprite>(characterStats.PortraitPath);
            }
            DeliveryLocationImage.sprite = sprite;
            DeliveryLocationText.text = displayName;
        }

        public void UpdateDisplay()
        {
            throw new System.NotImplementedException();
        }
    }
}
