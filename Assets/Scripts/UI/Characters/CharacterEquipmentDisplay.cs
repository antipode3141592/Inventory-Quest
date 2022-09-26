using Data;
using Data.Characters;
using Data.Items;
using InventoryQuest.Managers;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class CharacterEquipmentDisplay : MonoBehaviour
    {
        [SerializeField] WeaponProficiencySwitch weaponProficiencySwitch;

        IPartyManager _partyManager;
        PlayableCharacter _character;

        Dictionary<string, EquipmentSlotDisplay> _equipmentSlots = new();
        Dictionary<string, WeaponGroup> _weaponGroups = new();

        [Inject]
        public void Init(IPartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        void Awake()
        {
            var slots = GetComponentsInChildren<EquipmentSlotDisplay>(includeInactive: true);
            var weaponGroups = GetComponentsInChildren<WeaponGroup>(includeInactive: true);
            foreach (var slot in slots)
            {
                _equipmentSlots.Add(slot.SlotId, slot);
            }
            foreach (var group in weaponGroups)
            {
                _weaponGroups.Add(group.WeaponProficiencyName, group);
            }
            weaponProficiencySwitch.buttonPressed += SwitchWeaponProficiency;
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
            ShowActiveWeaponSlots();
        }

        void ShowActiveWeaponSlots()
        {
            foreach(var group in _weaponGroups)
            {
                if (group.Key == _character.CurrentWeaponProficiency.Name)
                {
                    group.Value.gameObject.SetActive(true);
                    foreach(var slot in group.Value.WeaponSlots)
                    {
                        slot.SetCharacter(_character);
                        slot.CheckIsOccupied();
                    }
                }
                else
                {
                    group.Value.gameObject.SetActive(false);
                }
            }
            weaponProficiencySwitch.UpdateText(_character.CurrentWeaponProficiency.Name);
        }

        void SwitchWeaponProficiency(object sender, EventArgs e)
        {
            _character.ChangeToNextWeapon();
            ShowActiveWeaponSlots();
        }
    }
}