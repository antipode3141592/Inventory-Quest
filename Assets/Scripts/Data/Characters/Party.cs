using Data.Items;
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
                foreach (var slot in character.Value.EquipmentSlots)
                {
                    var equippedItem = slot.Value.EquippedItem;
                    //advance to next slot if no item is equipped
                    if (slot.Value.EquippedItem is null)
                        continue;
                    //check if equipped item is container
                    if (equippedItem.Components.ContainsKey(typeof(IContainer)))
                    {
                        var _container = equippedItem.Components[typeof(IContainer)] as IContainer;
                        runningTotal = RemoveItemInContainer(itemId, runningTotal, amountToRemove, _container);
                    }

                    if (equippedItem.Id == itemId)
                    {
                        runningTotal += equippedItem.Quantity;
                        slot.Value.TryUnequip(out _);
                        if (runningTotal >= amountToRemove)
                            return runningTotal;
                    }
                }
            }
            if (Debug.isDebugBuild)
                Debug.Log($"RemoveItemFromPartyInventory({itemId}, {amountToRemove}) = x{runningTotal} removed");
            return runningTotal;
        }

        static double RemoveItemInContainer(string itemId, double runningTotal, double amountToRemove, IContainer container)
        {
            double _runningTotal = runningTotal;
            for (int i = 0; i < container.Contents.Count; i++)
            {
                var content = container.Contents.ElementAt(i);
                if (content.Value.Item.Components.ContainsKey(typeof(IContainer)))
                {
                    var _container = content.Value.Item.Components[typeof(IContainer)] as IContainer;
                    _runningTotal = RemoveItemInContainer(itemId, _runningTotal, amountToRemove, _container);
                }
                if (content.Value.Item.Id == itemId)
                {
                    int clampedAmount = Mathf.Clamp((int)amountToRemove, 1, content.Value.Item.Quantity);
                    content.Value.Item.Quantity -= clampedAmount;
                    _runningTotal += clampedAmount;
                    if (_runningTotal >= amountToRemove)
                        return _runningTotal;
                }
            }
            return _runningTotal;
        }

        public double CountItemInParty(string itemId)
        {
            double runningTotal = 0;
            foreach (var character in Characters.Values)
            {
                foreach (var slot in character.EquipmentSlots)
                {
                    var equippedItem = slot.Value.EquippedItem;
                    if (slot.Value.EquippedItem is null)
                        continue;
                    if (equippedItem.Components.ContainsKey(typeof(IContainer)))
                    {
                        var _container = equippedItem.Components[typeof(IContainer)] as IContainer;
                        runningTotal = CountItemInContainer(itemId, runningTotal, _container);
                    }
                    if (equippedItem.Id == itemId)
                        runningTotal += equippedItem.Quantity;   
                }
            }
            if (Debug.isDebugBuild)
                Debug.Log($"CountItemInParty({itemId}) = {runningTotal}");
            return runningTotal;
        }

        static double CountItemInContainer(string itemId, double runningTotal, IContainer container)
        {
            double _runningTotal = runningTotal;
            foreach (var content in container.Contents)
            {
                if (content.Value.Item.Id == itemId)
                {
                    _runningTotal += content.Value.Item.Quantity;
                }
                if (content.Value.Item.Components.ContainsKey(typeof(IContainer)))
                {
                    var _container = content.Value.Item.Components[typeof(IContainer)] as IContainer;
                    _runningTotal = CountItemInContainer(itemId, _runningTotal, _container);
                }
            }
            return _runningTotal;
        }
    }
}
