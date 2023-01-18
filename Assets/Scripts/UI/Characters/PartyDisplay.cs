using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class PartyDisplay: MonoBehaviour, IOnMenuShow
    {
        IPartyManager _partyManager;

        [SerializeField] bool interactable;
        [SerializeField] CharacterPortrait CharacterPortraitPrefab;
        [SerializeField] List<CharacterStatsDisplay> _characterStatsDisplays;
        [SerializeField] List<CharacterPortrait> PartyDisplayList;
        
                
        [Inject]
        public void Init(IPartyManager partyManager)
        {
            _partyManager = partyManager;
        }

        public void OnShow()
        {
            HidePortraits();
            SetPortraits();
            PartyMemberSelected(_partyManager.CurrentParty.SelectedPartyMemberGuId);
        }

        void SetPortraits()
        {
            for(int i = 0; i < _partyManager.CurrentParty.PartyDisplayOrder.Count; i++)
            {
                var character = _partyManager.CurrentParty.Characters[_partyManager.CurrentParty.PartyDisplayOrder[i]];
                PartyDisplayList[i].SetupPortrait(guid: character.GuId, displayName: character.DisplayName, sprite: character.Stats.Portrait, character: character, partyDisplay: this);
                PartyDisplayList[i].Show();
                if (_partyManager.CurrentParty.SelectedPartyMemberGuId == character.GuId)
                {
                    PartyDisplayList[i].IsSelected = true;
                    foreach (var display in _characterStatsDisplays)
                    {
                        display.CurrentCharacter = _partyManager.CurrentParty.SelectCharacter(character.GuId);
                    }
                } 
                else
                {
                    PartyDisplayList[i].IsSelected = false;
                }
            }
        }

        void HidePortraits()
        {
            for (int i = 0; i < PartyDisplayList.Count; i++)
                PartyDisplayList[i].Hide();
        } 
        
        public void PartyMemberSelected(string characterGuid)
        {
            foreach (var portrait in PartyDisplayList)
            {
                if (interactable && portrait.CharacterGuid == characterGuid)
                {
                    portrait.IsSelected = true;
                    var character = _partyManager.CurrentParty.SelectCharacter(characterGuid);
                    foreach (var display in _characterStatsDisplays)
                    {
                        display.CurrentCharacter = character;
                    }
                }
                else
                    portrait.IsSelected = false;
            }
        }
    }
}
