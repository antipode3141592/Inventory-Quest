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
        IPartyManager _partyManager;
        PlayableCharacter _character;

        Dictionary<EquipmentSlotType, EquipmentSlotDisplay> _equipmentSlots = new Dictionary<EquipmentSlotType, EquipmentSlotDisplay>();

        [Inject]
        public void Init(IPartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        private void Awake()
        {
            var slots = GetComponentsInChildren<EquipmentSlotDisplay>();
            foreach (var slot in slots)
            {
                _equipmentSlots.Add(slot.SlotType, slot);
            }   
        }

        private void Start()
        {
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelect;
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