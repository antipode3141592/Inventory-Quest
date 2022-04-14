using Data;
using Data.Characters;
using Data.Items;
using InventoryQuest.Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class CharacterEquipmentDisplay : MonoBehaviour
    {
        PartyManager _partyManager;
        GameManager _gameManager;
        PlayableCharacter _character;

        Dictionary<EquipmentSlotType, EquipmentSlotDisplay> _equipmentSlots = new Dictionary<EquipmentSlotType, EquipmentSlotDisplay>();

        [Inject]
        public void Init(GameManager gameManager, PartyManager partyManager)
        {
            _gameManager = gameManager;
            _partyManager = partyManager;
        }

        private void Awake()
        {
            var slots = GetComponentsInChildren<EquipmentSlotDisplay>();
            foreach (var slot in slots)
            {
                _equipmentSlots.Add(slot.SlotType, slot);
            }
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelect;
        }

        private void OnDestroy()
        {
            if (_partyManager.CurrentParty != null)
                _partyManager.CurrentParty.OnPartyMemberSelected -= OnPartyMemberSelect;
        }

        private void OnPartyMemberSelect(object sender, MessageEventArgs e)
        {
            _character = _partyManager.CurrentParty.Characters[e.Message];
            foreach(var slot in _equipmentSlots)
            {
                if (_character.EquipmentSlots.ContainsKey(slot.Key))
                {
                    slot.Value.gameObject.SetActive(true);
                    slot.Value.SetCharacter(_character);
                    slot.Value.CheckIsOccupied();
                }
                else
                {
                    slot.Value.gameObject.SetActive(false);
                }
            }
        }
    }
}