using InventoryQuest.Characters;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryQuest
{
    public class Party
    {
        public List<Character> Characters;

        public Party(List<Character> characters)
        {
            Characters = characters;
        }

        public Party(Character[] characters)
        {
            Characters = new List<Character>(characters);
        }

        public float PartyStrength => Characters.Sum(x => x.Stats.Strength.CurrentValue);
        public float PartyAttack => Characters.Sum(x => x.Stats.Attack.CurrentValue);

        // add character to party
        public void Recruit(Character character)
        {
            Characters.Add(character);
        }

        // remove character from party
        public void Dismiss(Character character)
        {
            Characters.Remove(character);
        }

        public void ReturnToTown(Character character)
        {
            
        }

        public void ReturnAllToTown()
        {

        }

        public int CountItemInParty(string itemName)
        {
            int partyCount = 0;
            foreach (Character character in Characters)
            {
                partyCount += character.PrimaryContainer.Contents.Count(x => x.Value.Item.Name == itemName);
                partyCount += character.EquipmentSlots.Count(x => x.Value.EquippedItem != null && x.Value.EquippedItem.Name == itemName);
                Debug.Log($"{character.Stats.Name} is carrying {partyCount} units of {itemName}");
            }
            return partyCount;
        }
    }
}
