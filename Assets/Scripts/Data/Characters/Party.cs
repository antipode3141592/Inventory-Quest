using System;
using System.Collections.Generic;

namespace Data
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


        public void AddCharacter(Character character)
        {
            if (character is null) return;
            Characters.Add(character.GuId, character);
            PartyDisplayOrder.Add(character.GuId);
            SelectedPartyMemberGuId = character.GuId;
        }
        public Character SelectCharacter(string characterId)
        {
            if (!Characters.ContainsKey(characterId)) return null;
            SelectedPartyMemberGuId = characterId;
            OnPartyMemberSelected?.Invoke(this, new MessageEventArgs(characterId));
            return Characters[characterId];
        }

        public void SetDisplayOrder()
        {

        }
    }
}
