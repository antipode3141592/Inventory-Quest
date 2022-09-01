using System;
using System.Collections.Generic;

namespace Data.Characters

{
    public class Party
    {
        public Dictionary<string, PlayableCharacter> Characters;
        public List<string> PartyDisplayOrder;

        public string SelectedPartyMemberGuId { get; set; }

        public EventHandler<MessageEventArgs> OnPartyMemberSelected;
        public EventHandler OnPartyMemberStatsUpdated;

        void OnStatsUpdatedHandler(object sender, EventArgs e)
        {
            OnPartyMemberStatsUpdated?.Invoke(this, e);
        }

        public Party(PlayableCharacter[] characters = null)
        {
            Characters = new Dictionary<string, PlayableCharacter>();
            PartyDisplayOrder = new List<string>();
            if (characters == null) return;
            foreach (PlayableCharacter character in characters)
            {
                Characters.Add(character.GuId, character);
                PartyDisplayOrder.Add(character.GuId);
                character.OnStatsUpdated += OnStatsUpdatedHandler;
            }
            SelectedPartyMemberGuId = PartyDisplayOrder[0];
        }


        public void AddCharacter(PlayableCharacter character)
        {
            if (character is null) return;
            Characters.Add(character.GuId, character);
            PartyDisplayOrder.Add(character.GuId);
            SelectedPartyMemberGuId = character.GuId;
            character.OnStatsUpdated += OnStatsUpdatedHandler;
        }
        public PlayableCharacter SelectCharacter(string characterId)
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
