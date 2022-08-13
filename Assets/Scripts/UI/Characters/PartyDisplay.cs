using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class PartyDisplay: MonoBehaviour, IOnMenuShow
    {
        [SerializeField]
        GameObject CharacterPortraitPrefab;

        CharacterStatsDisplay _characterStatsDisplay;

        List<CharacterPortrait> PartyDisplayList;

        IPartyManager _partyManager;
        
                
        [Inject]
        public void Init(CharacterStatsDisplay characterStatsDisplay, IPartyManager partyManager)
        {
            _characterStatsDisplay = characterStatsDisplay;
            _partyManager = partyManager;
        }

        void Awake()
        {
            PartyDisplayList = new List<CharacterPortrait>();
            
        }

        public void OnShow()
        {
            PartyMemberSelected(_partyManager.CurrentParty.SelectedPartyMemberGuId);
        }

        private void Start()
        {
            SetPortraits();
        }

        public void SetPortraits()
        {
            for(int i = 0; i < _partyManager.CurrentParty.PartyDisplayOrder.Count; i++)
            {
                if (_partyManager.CurrentParty.PartyDisplayOrder.Count > PartyDisplayList.Count)
                {
                    CharacterPortrait portrait = Instantiate(CharacterPortraitPrefab, transform).GetComponent<CharacterPortrait>();
                    PartyDisplayList.Add(portrait);
                }
                var character = _partyManager.CurrentParty.Characters[_partyManager.CurrentParty.PartyDisplayOrder[i]];
                PartyDisplayList[i].SetupPortrait(guid: character.GuId, displayName: character.Stats.DisplayName, imagePath: character.Stats.PortraitPath);
                PartyDisplayList[i].PartyDisplay = this;
                if (_partyManager.CurrentParty.SelectedPartyMemberGuId == character.GuId)
                {
                    PartyDisplayList[i].IsSelected = true;
                    _characterStatsDisplay.CurrentCharacter = _partyManager.CurrentParty.SelectCharacter(character.GuId);
                } else
                {
                    PartyDisplayList[i].IsSelected = false;
                }

            }
        }
        
        public void PartyMemberSelected(string characterGuid)
        {
            foreach (var portrait in PartyDisplayList)
            {
                if (portrait.CharacterGuid == characterGuid)
                {
                    portrait.IsSelected = true;
                    var character = _partyManager.CurrentParty.SelectCharacter(characterGuid);
                    _characterStatsDisplay.CurrentCharacter = character;
                }
                else
                    portrait.IsSelected = false;
            }
        }
    }
}
