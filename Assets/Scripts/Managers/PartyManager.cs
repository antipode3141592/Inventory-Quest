﻿using Data.Characters;
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

        readonly Party _party = new();

        public Party CurrentParty => _party;

        [Inject]
        public void Init(ICharacterDataSource characterDataSource, IItemDataSource itemDataSource)
        {
            _itemDataSource = itemDataSource;
            _characterDataSource = characterDataSource;
        }

        void Start()
        {
            AddCharacterWithEquipmentToParty("player", 
                startingEquipment: new List<string>
                {
                    "adventure_backpack",
                    "sandal_1",
                    "shirt_1",
                    "pants_1"
                },
                startingInventory: new List<string>
                {
                    "apple_fuji",
                    "apple_fuji",
                    "apple_fuji"
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
                AddCharacterWithEquipmentToParty(id,
                    startingEquipment: equipment,
                    startingInventory: inventory);
                return;
            }
            AddCharacterWithEquipmentToParty(id);


        }

        public void AddCharacterToParty(ICharacter character)
        {
            _party.AddCharacter(character);
        }

        void AddCharacterWithEquipmentToParty(string id, IList<string> startingEquipment = null, IList<string> startingInventory = null)
        {
            List<IItem> equipment = new();
            List<IItem> items = new();

            

            if (startingEquipment is not null)
            {
                foreach(var _equipment in startingEquipment)
                {
                    equipment.Add(ItemFactory.GetItem(_itemDataSource.GetById(_equipment)));
                }
            }
            if (startingInventory is not null)
            {
                foreach(var _item in startingInventory)
                {
                    items.Add(ItemFactory.GetItem(_itemDataSource.GetById(_item)));
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
