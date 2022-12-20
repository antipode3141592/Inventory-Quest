using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    public class Party
    {
        public Dictionary<string, ICharacter> Characters;
        public List<string> PartyDisplayOrder;

        public string SelectedPartyMemberGuId { get; set; }

        public event EventHandler<string> OnPartyMemberSelected;
        public event EventHandler OnPartyMemberStatsUpdated;
        public event EventHandler OnPartyCompositionChanged;
        public event EventHandler<string> OnItemAddedToPartyInventory;

        public event EventHandler OnPartyDeath;

        void OnStatsUpdatedHandler(object sender, EventArgs e)
        {
            OnPartyMemberStatsUpdated?.Invoke(this, e);
        }

        public Party(ICharacter[] characters = null)
        {
            Characters = new Dictionary<string, ICharacter>();
            PartyDisplayOrder = new List<string>();
            if (characters == null) return;
            foreach (ICharacter character in characters)
            {
                Characters.Add(character.GuId, character);
                PartyDisplayOrder.Add(character.GuId);
                character.OnStatsUpdated += OnStatsUpdatedHandler;
                character.OnDead += OnCharacterDeath;
                character.OnItemAddedToBackpack += OnItemAddedToCharacterBackpack;
            }
            SelectedPartyMemberGuId = PartyDisplayOrder[0];
        }

        void OnCharacterDeath(object sender, EventArgs e)
        {
            Debug.Log($"OnCharacterDeath in Party...");
            if (PartyIsDead())
                OnPartyDeath?.Invoke(this, EventArgs.Empty);
        }

        bool PartyIsDead()
        {
            foreach(var character in Characters.Values)
                if (!character.IsDead)
                    return false;
            return true;
        }

        public void AddCharacter(ICharacter character)
        {
            if (character is null) return;
            Characters.Add(character.GuId, character);
            PartyDisplayOrder.Add(character.GuId);
            SelectedPartyMemberGuId = character.GuId;
            character.OnStatsUpdated += OnStatsUpdatedHandler;
            character.OnDead += OnCharacterDeath;
            character.OnItemAddedToBackpack += OnItemAddedToCharacterBackpack;
            OnPartyCompositionChanged?.Invoke(this, EventArgs.Empty);
        }

        void OnItemAddedToCharacterBackpack(object sender, string e)
        {
            
        }

        public ICharacter SelectCharacter(string characterGuId)
        {
            if (!Characters.ContainsKey(characterGuId)) return null;
            SelectedPartyMemberGuId = characterGuId;
            OnPartyMemberSelected?.Invoke(this, characterGuId);
            return Characters[characterGuId];
        }

        public void SetDisplayOrder()
        {

        }

        public void RemoveItemFromPartyInventory(string itemId, double minToRemove)
        {
            double runningTotal = 0;
            foreach (var character in Characters)
            {
                foreach (var content in character.Value.Backpack.Contents)
                {
                    if (content.Value.Item.Id == itemId)
                    {
                        runningTotal += content.Value.Item.Quantity;
                        character.Value.Backpack.TryTake(out _, content.Value.GridSpaces[0]);
                        if (runningTotal >= minToRemove)
                            return;
                    }
                }
                foreach (var slot in character.Value.EquipmentSlots)
                {
                    var equippedItem = slot.Value.EquippedItem;
                    if (slot.Value.EquippedItem is not null)
                    {
                        if (equippedItem.Id == itemId)
                        {
                            runningTotal += equippedItem.Quantity;
                            slot.Value.TryUnequip(out _);
                            if (runningTotal >= minToRemove)
                                return;
                        }
                    }
                }
            }
        }

        public double CountItemInParty(string itemId)
        {
            double runningTotal = 0;
            foreach (var character in Characters.Values)
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
                    var equippedItem = slot.Value.EquippedItem;
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
