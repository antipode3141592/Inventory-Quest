using Data.Characters;
using Data.Items;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PartyManager : SerializedMonoBehaviour, IPartyManager
    {
        [SerializeField] List<ICharacterStats> InitialPartyMembers;

        ICharacterDataSource _characterDataSource;
        IGameManager _gameManager;

        readonly Party _party = new();

        public Party CurrentParty => _party;

        public bool IsPartyDead { get; set; }

        [Inject]
        public void Init(ICharacterDataSource characterDataSource, IGameManager gameManager)
        {
            _characterDataSource = characterDataSource;
            _gameManager = gameManager;
        }

        void Start()
        {
            Lua.RegisterFunction("AddCharacterToPartyById", this, SymbolExtensions.GetMethodInfo(() => AddCharacterToPartyById(string.Empty)));
            Lua.RegisterFunction("RemoveCharacterFromPartyById", this, SymbolExtensions.GetMethodInfo(() => RemoveCharacterFromPartyById(string.Empty)));
            Lua.RegisterFunction("IsCharacterIdInParty", this, SymbolExtensions.GetMethodInfo(() => IsCharacterIdInParty(string.Empty)));

            CurrentParty.OnPartyDeath += PartyDeathHandler;
            _gameManager.OnGameBeginning += OnGameBeginningHandler;
        }

        void OnGameBeginningHandler(object sender, EventArgs e)
        {
            InitializeParty();
        }

        void InitializeParty()
        {
            for (int i = 0; i < InitialPartyMembers.Count; i++)
                AddCharacterToParty(InitialPartyMembers[i]);
        }

        IEnumerator DestroyParty()
        {
            yield return null;
            IsPartyDead = true;
            while (CurrentParty.Characters.Count > 0)
                CurrentParty.RemoveCharacter(CurrentParty.Characters.First().Key);
        }

        void PartyDeathHandler(object sender, EventArgs e)
        {
            Debug.Log($"PartyDeathHandler on {gameObject.name}...", this);
            StartCoroutine(DestroyParty());
        }

        public void AddCharacterToPartyById(string id)
        {
            var characterStats = _characterDataSource.GetById(id);
            QuestLog.Log($"{characterStats.Name} has joined the party.");
            AddCharacterToParty(characterStats);
        }

        public void RemoveCharacterFromPartyById(string id)
        {
            var character = _party.Characters.Values.FirstOrDefault(x => x.Stats.Id == id);
            if (character is null)
                return;
            if (!_party.Characters.ContainsKey(character.GuId))
                return;
            QuestLog.Log($"{character.DisplayName} has left the party.");
            _party.RemoveCharacter(character.GuId);
        }

        public bool IsCharacterIdInParty(string id)
        {
            var character = _party.Characters.Values.FirstOrDefault(x => x.Stats.Id == id);
            if (character is null)
                return false;
            return true;
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
                int quantityRemaining = _item.Item2;
                int stackCount = 1;
                while (quantityRemaining > 0)
                {
                    var __item = ItemFactory.GetItem(_item.Item1);
                    int quantity = quantityRemaining < _item.Item1.MaxQuantity ? quantityRemaining : _item.Item1.MaxQuantity;
                    __item.Quantity = quantity;
                    quantityRemaining -= quantity;
                    Debug.Log($"... stack {stackCount++}, quantity {__item.Quantity}");
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
                        runningTotal += content.Value.Item.Quantity;
                }
                foreach (var slot in character.EquipmentSlots)
                {
                    var equippedItem = slot.Value.EquippedItem as IItem;
                    if (slot.Value.EquippedItem is not null)
                        if (equippedItem.Id == itemId)
                            runningTotal += equippedItem.Quantity;
                }
            }
            return runningTotal;
        }
    }
}
