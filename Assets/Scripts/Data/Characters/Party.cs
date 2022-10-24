using System;
using System.Collections.Generic;

namespace Data.Characters

{
    public class Party
    {
        public Dictionary<string, ICharacter> Characters;
        public List<string> PartyDisplayOrder;

        public string SelectedPartyMemberGuId { get; set; }

        public event EventHandler<string> OnPartyMemberSelected;
        public event EventHandler OnPartyMemberStatsUpdated;
        public event  EventHandler OnPartyCompositionChanged;
        public event EventHandler<string> OnItemAddedToPartyInventory;

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
            }
            SelectedPartyMemberGuId = PartyDisplayOrder[0];
        }


        public void AddCharacter(ICharacter character)
        {
            if (character is null) return;
            Characters.Add(character.GuId, character);
            PartyDisplayOrder.Add(character.GuId);
            SelectedPartyMemberGuId = character.GuId;
            character.OnStatsUpdated += OnStatsUpdatedHandler;
            character.OnItemAddedToBackpack += OnItemAddedToCharacterBackpack;
            OnPartyCompositionChanged?.Invoke(this, EventArgs.Empty);
        }

        void OnItemAddedToCharacterBackpack(object sender, string e)
        {
            
        }

        public ICharacter SelectCharacter(string characterId)
        {
            if (!Characters.ContainsKey(characterId)) return null;
            SelectedPartyMemberGuId = characterId;
            OnPartyMemberSelected?.Invoke(this, characterId);
            return Characters[characterId];
        }

        public void SetDisplayOrder()
        {

        }
    }
}
