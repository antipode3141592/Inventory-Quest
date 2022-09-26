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

        [Inject]
        public void Init(IPartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        void Awake()
        {
            var slots = GetComponentsInChildren<EquipmentSlotDisplay>(includeInactive: true);
            foreach (var slot in slots)
            {
                _equipmentSlots.Add(slot.SlotId, slot);
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
            foreach (var s in _equipmentSlots)
                s.Value.gameObject.SetActive(true);
            foreach (var prof in _character.WeaponProficiencies)
            {
                foreach (var slot in prof.EquipmentSlots)
                {
                    string v = slot.ToString().ToLower();
                    Debug.Log($"prof slot: {v}");
                    var _slot = _equipmentSlots.Values.First(x => x.SlotId == v);
                    if (prof.Name == _character.CurrentWeaponProficiency.Name)
                        _slot.gameObject.SetActive(false);
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