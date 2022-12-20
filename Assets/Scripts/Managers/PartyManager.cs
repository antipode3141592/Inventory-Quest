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
            foreach(var _equipment in characterStats.StartingEquipment)
            {
                startingEquipment.Add(ItemFactory.GetItem(_equipment));
            }
            foreach(var _item in characterStats.StartingInventory)
            {
                startingInventory.Add(ItemFactory.GetItem(_item));
            }
            var _character = (PlayableCharacter)CharacterFactory.GetCharacter(
                baseStats: characterStats,
                startingEquipment: startingEquipment,
                startingInventory: startingInventory
            );
            _party.AddCharacter(_character);
        }
    }
}
