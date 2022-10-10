using Data.Characters;
using Data.Items;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PartyManager : MonoBehaviour, IPartyManager
    {
        ICharacterDataSource _characterDataSource;
        IItemDataSource _itemDataSource;

        Party _party = new();

        public Party CurrentParty => _party;

        [Inject]
        public void Init(ICharacterDataSource characterDataSource, IItemDataSource itemDataSource)
        {
            _itemDataSource = itemDataSource;
            _characterDataSource = characterDataSource;
        }

        void Awake()
        {
            AddCharacterWithEquipmentToParty("Player", 
                startingEquipment: new List<string>
                {
                    "adventure backpack",
                    "sandal_1",
                    "shirt_1",
                    "pants_1"
                },
                startingInventory: new List<string>
                {
                    "apple_fuji",
                    "apple_fuji",
                    "apple_fuji",
                    "ore_bloom_common",
                    "ore_bloom_common",
                    "ore_bloom_common"
                }
            );

            Lua.RegisterFunction("AddCharacterToParty", this, SymbolExtensions.GetMethodInfo(() => AddCharacterToParty(string.Empty)));
        }

        public void AddCharacterToParty(string id)
        {
            List<string> equipment = new();
            List<string> inventory = new();
            if (id == "wagon")
            {
                equipment.Add("wagon_standard");
                inventory.Add("log_standard");
                inventory.Add("log_standard");
                AddCharacterWithEquipmentToParty(id,
                    startingEquipment: equipment,
                    startingInventory: inventory);
                return;
            }
            AddCharacterWithEquipmentToParty(id);


        }

        void AddCharacterWithEquipmentToParty(string id, IList<string> startingEquipment = null, IList<string> startingInventory = null)
        {
            List<IEquipable> equipment = new();
            List<IItem> items = new();

            

            if (startingEquipment is not null)
            {
                foreach(var _equipment in startingEquipment)
                {
                    equipment.Add((IEquipable)ItemFactory.GetItem(_itemDataSource.GetItemStats(_equipment)));
                }
            }
            if (startingInventory is not null)
            {
                foreach(var _item in startingInventory)
                {
                    items.Add(ItemFactory.GetItem(_itemDataSource.GetItemStats(_item)));
                }
            }

            var _character = (PlayableCharacter)CharacterFactory.GetCharacter(baseStats: _characterDataSource.GetById(id),
                startingEquipment: equipment,
                startingInventory: items
            );
            _party.AddCharacter(_character);
        }
    }
}
