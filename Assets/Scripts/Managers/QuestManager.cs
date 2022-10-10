using Data;
using Data.Characters;
using Data.Items;
using PixelCrushers.DialogueSystem;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class QuestManager : MonoBehaviour, IQuestManager
    {
        IPartyManager _partyManager;
        IItemDataSource _itemDataSource;

        Party _party => _partyManager.CurrentParty;

        [Inject]
        public void Init(IPartyManager partyManager, IItemDataSource itemDataSource)
        {
            _partyManager = partyManager;
            _itemDataSource = itemDataSource;
        }

        void Start()
        {
            Lua.RegisterFunction("AddItemToParty", this, SymbolExtensions.GetMethodInfo(() => AddItemToPartyInventory(string.Empty)));
            Lua.RegisterFunction("CountItemInParty", this, SymbolExtensions.GetMethodInfo(() => CountItemInPartyInventory(string.Empty)));
            Lua.RegisterFunction("RemoveItemFromParty", this, SymbolExtensions.GetMethodInfo(() => RemoveItemFromPartyInventory(string.Empty, 0)));
        }

        public void AddItemToPartyInventory(string itemId)
        {
            var item = ItemFactory.GetItem(_itemDataSource.GetItemStats(itemId));
            if (item is null) return;
            foreach(var character in _partyManager.CurrentParty.Characters)
                if (ItemPlacementHelpers.TryAutoPlaceToContainer(character.Value.Backpack, item))
                    return;
        }

        public double CountItemInPartyInventory(string itemId)
        {
            double runningTotal = 0;
            foreach (var character in _partyManager.CurrentParty.Characters)
            {
                foreach (var content in character.Value.Backpack.Contents)
                {
                    if (content.Value.Item.Id == itemId)
                    {
                        runningTotal += content.Value.Item.Quantity;
                    }
                }
                foreach (var slot in character.Value.EquipmentSlots)
                {
                    var equippedItem = slot.Value.EquippedItem as IItem;
                    if (slot.Value.EquippedItem is not null)
                    {
                        if (equippedItem.Id == itemId)
                        {
                            runningTotal += equippedItem.Quantity;
                        }

                    }
                }
            }
            return runningTotal;
        }

        public void RemoveItemFromPartyInventory(string itemId, double minToRemove)
        {
            double runningTotal = 0;
            foreach (var character in _partyManager.CurrentParty.Characters)
            {
                foreach (var content in character.Value.Backpack.Contents)
                {
                    if (content.Value.Item.Id == itemId)
                    {
                        runningTotal += content.Value.Item.Quantity;
                        character.Value.Backpack.TryTake(out _, content.Value.GridSpaces[0]);
                        if (runningTotal >= minToRemove)
                            return;
                    }
                }
                foreach (var slot in character.Value.EquipmentSlots)
                {
                    var equippedItem = slot.Value.EquippedItem as IItem;
                    if (slot.Value.EquippedItem is not null)
                    {
                        if (equippedItem.Id == itemId)
                        {
                            runningTotal += equippedItem.Quantity;
                            slot.Value.TryUnequip(out _);
                            if (runningTotal >= minToRemove)
                                return;
                        }

                    }
                }
            }
        }
    }
}
