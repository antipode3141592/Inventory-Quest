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

        Dictionary<string, EquipmentSlotDisplay> _equipmentSlots = new();

        [Inject]
        public void Init(IPartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        void Awake()
        {
            var slots = GetComponentsInChildren<EquipmentSlotDisplay>();
            foreach (var slot in slots)
            {
                _equipmentSlots.Add(slot.SlotId, slot);
            }   
        }

        void Start()
        {
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelect;
        }

        void OnPartyMemberSelect(object sender, MessageEventArgs e)
        {
            _character = _partyManager.CurrentParty.Characters[e.Message];

            //TODO display proper template for species

            foreach (var slot in _equipmentSlots)
                //foreach (var slotDisplay in _character.EquipmentSlots)
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