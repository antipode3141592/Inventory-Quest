using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data.Characters
{
    public class Party
    {
        public Dictionary<string, ICharacter> Characters;
        public List<string> PartyDisplayOrder;
        public List<string> FrontRow { get; } = new();
        public List<string> SupportRow { get; } = new();


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
            {
                Debug.Log($"All party members dead!");
                OnPartyDeath?.Invoke(this, EventArgs.Empty);
                return;
            }
            Debug.Log($"At least one party member is still alive!");
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

        public void RemoveCharacter(string characterGuId)
        {
            Debug.Log($"RemoveCharacter(Guid: {characterGuId})...");
            var character = Characters.ContainsKey(characterGuId) ? Characters[characterGuId] : null;
            if (character is null) return;
            //unsubscribe
            character.OnStatsUpdated -= OnStatsUpdatedHandler;
            character.OnDead -= OnCharacterDeath;
            character.OnItemAddedToBackpack -= OnItemAddedToCharacterBackpack;
            //remove all carried items and all equipment
            while (character.Backpack.Contents.Count > 0)
                character.Backpack.TryTake(out _, character.Backpack.Contents.First().Value.GridSpaces[0]);
            foreach (var equipment in character.EquipmentSlots)
                equipment.Value.TryUnequip(out _);
            //remove character from party list
            Characters.Remove(characterGuId);
            if (PartyDisplayOrder.Contains(characterGuId))
                PartyDisplayOrder.Remove(characterGuId);
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

        public double RemoveItemFromPartyInventory(string itemId, double amountToRemove)
        {
            double runningTotal = 0;
            foreach (var character in Characters)
            {
                //foreach (var content in character.Value.Backpack.Contents)
                for (int i = 0; i < character.Value.Backpack.Contents.Count; i++)
                {
                    var content = character.Value.Backpack.Contents.ElementAt(i);
                    if (content.Value.Item.Id == itemId)
                    {
                        int clampedAmount = Mathf.Clamp((int)amountToRemove, 0, content.Value.Item.Quantity);
                        content.Value.Item.Quantity -= clampedAmount;
                        runningTotal += clampedAmount;
                        if (runningTotal >= amountToRemove)
                            return runningTotal;
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
                            if (runningTotal >= amountToRemove)
                                return runningTotal;
                        }
                    }
                }
            }
            return runningTotal;
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
