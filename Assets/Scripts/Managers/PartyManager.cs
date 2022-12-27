using Data.Characters;
using Data.Items;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PartyManager : SerializedMonoBehaviour, IPartyManager
    {
        [SerializeField] List<ICharacterStats> InitialPartyMembers;

        ICharacterDataSource _characterDataSource;

        readonly Party _party = new();

        public Party CurrentParty => _party;

        [Inject]
        public void Init(ICharacterDataSource characterDataSource)
        {
            _characterDataSource = characterDataSource;
        }

        void Start()
        {
            for (int i = 0; i < InitialPartyMembers.Count; i++)
                AddCharacterToParty(InitialPartyMembers[i]);

            Lua.RegisterFunction("AddCharacterToPartyById", this, SymbolExtensions.GetMethodInfo(() => AddCharacterToPartyById(string.Empty)));

            CurrentParty.OnPartyDeath += PartyDeathHandler;
        }

        void PartyDeathHandler(object sender, EventArgs e)
        {
            Debug.Log($"PartyDeathHandler on {gameObject.name}...", this);
        }

        public void AddCharacterToPartyById(string id)
        {
            var characterStats = _characterDataSource.GetById(id);
            AddCharacterToParty(characterStats);
        }

        public void AddCharacterToParty(ICharacterStats characterStats)
        {
            List<IItem> equipment = UnityEngine.Pool.ListPool<IItem>.Get();
            List<IItem> inventory = UnityEngine.Pool.ListPool<IItem>.Get();

            AddCharacterWithEquipmentToParty(characterStats, equipment, inventory);

            UnityEngine.Pool.ListPool<IItem>.Release(equipment);
            UnityEngine.Pool.ListPool<IItem>.Release(inventory);
        }

        void AddCharacterWithEquipmentToParty(ICharacterStats characterStats, List<IItem> startingEquipment, List<IItem> startingInventory)
        {
            Debug.Log($"Adding Character {characterStats.Id} to party...");
            foreach(var _equipment in characterStats.StartingEquipment)
            {
                Debug.Log($"Equipping {_equipment.Id}...");
                startingEquipment.Add(ItemFactory.GetItem(_equipment));
            }
            foreach(var _item in characterStats.StartingInventory)
            {
                int stacksToMake = Mathf.CeilToInt((float)_item.Item2 / (float)_item.Item1.MaxQuantity);
                Debug.Log($"Adding {_item.Item1.Id} x{_item.Item2} (x{stacksToMake} stacks) to backpack...");
                for (int i = 0; i < stacksToMake; i++)
                {
                    var __item = ItemFactory.GetItem(_item.Item1);
                    __item.Quantity =  i < stacksToMake - 1 ? __item.Stats.MaxQuantity : (_item.Item2 % __item.Stats.MaxQuantity);
                    Debug.Log($"... stack {i}, quantity {__item.Quantity}");
                    startingInventory.Add(__item);
                }
            }
            var _character = (PlayableCharacter)CharacterFactory.GetCharacter(
                baseStats: characterStats,
                startingEquipment: startingEquipment,
                startingInventory: startingInventory
            );
            _party.AddCharacter(_character);
        }

        public double CountItemInCharacterInventories(string itemId)
        {
            double runningTotal = 0;
            foreach (var character in CurrentParty.Characters.Values)
            {
                foreach (var content in character.Backpack.Contents)
                {
                    if (content.Value.Item.Id == itemId)
                    {
                        runningTotal += content.Value.Item.Quantity;
                    }
                }
                foreach (var slot in character.EquipmentSlots)
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
    }
}
