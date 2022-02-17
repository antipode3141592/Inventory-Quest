using Data;
using InventoryQuest.Characters;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class CharacterEquipmentDisplay : MonoBehaviour
    {
        Party _party;
        GameManager _gameManager;
        Character _character;

        Dictionary<EquipmentSlotType, EquipmentSlotDisplay> _equipmentSlots = new Dictionary<EquipmentSlotType, EquipmentSlotDisplay>();

        [Inject]
        public void Init(GameManager gameManager, Party party)
        {
            _gameManager = gameManager;
            _party = party;
        }

        private void Awake()
        {
            var slots = GetComponentsInChildren<EquipmentSlotDisplay>();
            foreach (var slot in slots)
            {
                _equipmentSlots.Add(slot.SlotType, slot);
            }
            _party.OnPartyMemberSelected += OnPartyMemberSelect;
        }

        private void OnDestroy()
        {
            if (_party != null)
                _party.OnPartyMemberSelected -= OnPartyMemberSelect;
        }

        private void OnPartyMemberSelect(object sender, MessageEventArgs e)
        {
            _character = _party.Characters[e.Message];
            foreach(var slot in _equipmentSlots)
            {
                if (_character.EquipmentSlots.ContainsKey(slot.Key))
                {
                    slot.Value.gameObject.SetActive(true);
                    slot.Value.SetCharacter(_character);
                }
                else
                {
                    slot.Value.gameObject.SetActive(false);
                }
            }
        }
    }
}