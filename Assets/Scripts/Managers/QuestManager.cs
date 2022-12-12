using Data;
using Data.Characters;
using Data.Encounters;
using Data.Items;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class QuestManager : MonoBehaviour, IQuestManager
    {
        IPartyManager _partyManager;
        IItemDataSource _itemDataSource;

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
            var item = ItemFactory.GetItem(_itemDataSource.GetById(itemId));
            if (item is null) return;
            foreach(var character in _partyManager.CurrentParty.Characters)
                if (ItemPlacementHelpers.TryAutoPlaceToContainer(character.Value.Backpack, item))
                    return;
        }



        public double CountItemInPartyInventory(string itemId)
        {
            return EncounterResolutionHelpers.CountItemInCharacterInventories((IEnumerable<ICharacter>)_partyManager.CurrentParty, itemId);
        }

        

        public void RemoveItemFromPartyInventory(string itemId, double minToRemove)
        {
            _partyManager.CurrentParty.RemoveItemFromPartyInventory(itemId, minToRemove);
        }
    }
}
