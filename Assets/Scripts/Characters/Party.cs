﻿using Data;
using InventoryQuest.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryQuest
{
    public class Party
    {
        public Dictionary<string, Character> Characters;
        public List<string> PartyDisplayOrder;

        public string SelectedPartyMemberGuId { get; set; }

        public EventHandler<MessageEventArgs> OnPartyMemberSelected;

        public Party(Dictionary<string, Character> characters)
        {
            Characters = characters;
        }

        public Party(Character[] characters = null)
        {
            Characters = new Dictionary<string, Character>();
            PartyDisplayOrder = new List<string>();
            if (characters == null) return;
            foreach (Character character in characters)
            {
                Characters.Add(character.GuId, character);
                PartyDisplayOrder.Add(character.GuId);
            }
            SelectedPartyMemberGuId = PartyDisplayOrder[0];
        }

        public float PartyStrength => Characters.Sum(x => x.Value.Stats.Strength.CurrentValue);
        public float PartyAttack => Characters.Sum(x => x.Value.Stats.Attack.CurrentValue);

        // add character to party
        public void Recruit(Character character)
        {
            Characters.Add(character.GuId, character);
        }

        // remove character from party
        public void Dismiss(Character character)
        {
            Characters.Remove(character.GuId);
        }

        public void ReturnToTown(Character character)
        {

        }

        public void ReturnAllToTown()
        {

        }

        public Character SelectCharacter(string characterId)
        {
            if (!Characters.ContainsKey(characterId)) return null;
            SelectedPartyMemberGuId = characterId;
            OnPartyMemberSelected?.Invoke(this, new MessageEventArgs(characterId));
            return Characters[characterId];
        }

        public int GetPartyItemCountById(string id)
        {
            int partyCount = 0;
            foreach (var character in Characters)
            {
                partyCount += character.Value.GetItemCountById(id);
            }
            return partyCount;
        }

        public void SetDisplayOrder()
        {

        }
    }
}
