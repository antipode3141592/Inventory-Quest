using Data.Items;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class QuestManager : MonoBehaviour, IQuestManager
    {
        IPartyManager _partyManager;
        IItemDataSource _itemDataSource;
        IContainerManager _containerManager;

        public event EventHandler<IContainer> OnQuestContainerAvailable;

        [Inject]
        public void Init(IPartyManager partyManager, IItemDataSource itemDataSource, IContainerManager containerManager)
        {
            _partyManager = partyManager;
            _itemDataSource = itemDataSource;
            _containerManager = containerManager;
        }

        void Start()
        {
            Lua.RegisterFunction("AddItemToParty", this, SymbolExtensions.GetMethodInfo(() => AddItemToPartyInventory(string.Empty)));
            Lua.RegisterFunction("CountItemInParty", this, SymbolExtensions.GetMethodInfo(() => CountItemInPartyInventory(string.Empty)));
            Lua.RegisterFunction("RemoveItemFromParty", this, SymbolExtensions.GetMethodInfo(() => RemoveItemFromPartyInventory(string.Empty, 0)));
            Lua.RegisterFunction("UpdateQuestCounter", this, SymbolExtensions.GetMethodInfo(() => UpdateQuestItemCounter(string.Empty, string.Empty)));
            Lua.RegisterFunction("TrackItemQuantity", this, SymbolExtensions.GetMethodInfo(() => TrackItemQuantity(string.Empty, string.Empty, 0)));
            Lua.RegisterFunction("OfferQuestContainer", this, SymbolExtensions.GetMethodInfo(() => OfferQuestContainer(string.Empty, string.Empty, 0)));
        }

        public void AddItemToPartyInventory(string itemId)
        {
            var item = ItemFactory.GetItem(_itemDataSource.GetById(itemId));
            if (item is null) return;
            foreach (var character in _partyManager.CurrentParty.Characters)
                if (ItemPlacementHelpers.TryAutoPlaceToContainer(character.Value.Backpack, item))
                {
                    QuestLog.Log($"{item.DisplayName} added to {character.Value.DisplayName}'s inventory.");
                    return;
                }
        }

        public double CountItemInPartyInventory(string itemId)
        {
            return _partyManager.CountItemInParty(itemId);
        }

        public void UpdateQuestItemCounter(string itemId, string questCounter)
        {
            MessageSystem.SendMessage(this, DataSynchronizer.DataSourceValueChangedMessage, questCounter, CountItemInPartyInventory(itemId));
        }

        public void RemoveItemFromPartyInventory(string itemId, double minToRemove)
        {
            int qtyRemoved = (int)_partyManager.CurrentParty.RemoveItemFromPartyInventory(itemId, minToRemove);
            
            QuestLog.Log($"{_itemDataSource.GetById(itemId).Name} x{qtyRemoved} removed from party inventory.");
        }

        public void TrackItemQuantity(string questCounter, string itemId, double targetQuantity)
        {
            Debug.Log($"Begin tracking {itemId} to quest counter id {questCounter}, target quantity: {targetQuantity}");
            StartCoroutine(ItemTracking(questCounter, itemId, targetQuantity));
        }

        IEnumerator ItemTracking(string questCounter, string itemId, double targetQuantity)
        {
            double currentQuantity;
            do
            {
                yield return new WaitForSeconds(1f);
                currentQuantity = CountItemInPartyInventory(itemId);
                MessageSystem.SendMessage(this, DataSynchronizer.DataSourceValueChangedMessage, questCounter, currentQuantity);
            } while (currentQuantity >= targetQuantity);
            Debug.Log($"QuestCounter {questCounter} has reached target quantity: {targetQuantity}");
        }

        public void OfferQuestContainer(string containerItemId, string startingItemId, double startingItemQuantity)
        {
            var container = _containerManager.AddNewContainer(containerItemId);
            if (startingItemId != string.Empty && startingItemQuantity >= 0)
            {
                IItem item = ItemFactory.GetItem(_itemDataSource.GetById(startingItemId));
                if (item is null) return;
                if (item.Stats.IsStackable)
                    item.Quantity = Mathf.Clamp((int)startingItemQuantity, 1, item.Stats.MaxQuantity);
                ItemPlacementHelpers.TryAutoPlaceToContainer(container, item);
            }
        }
    }
}
