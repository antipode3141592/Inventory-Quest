using InventoryQuest.Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class PartyDisplay: MonoBehaviour
    {
        [SerializeField]
        GameObject CharacterPortraitPrefab;

        GameManager _gameManager;
        ContainerDisplayManager _containerDisplayManager;
        CharacterStatsDisplay _characterStatsDisplay;

        List<CharacterPortrait> PartyDisplayList;

        PartyManager _partyManager;
        
                
        [Inject]
        public void Init(GameManager gameManager, ContainerDisplayManager containerDisplayManager, CharacterStatsDisplay characterStatsDisplay, PartyManager partyManager)
        {
            _gameManager = gameManager;
            _containerDisplayManager = containerDisplayManager;
            _characterStatsDisplay = characterStatsDisplay;
            _partyManager = partyManager;
        }

        void Awake()
        {
            PartyDisplayList = new List<CharacterPortrait>();
            
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
