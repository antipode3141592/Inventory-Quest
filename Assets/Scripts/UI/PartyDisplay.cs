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

        Party _party;
        
                
        [Inject]
        public void Init(GameManager gameManager, ContainerDisplayManager containerDisplayManager, CharacterStatsDisplay characterStatsDisplay, Party party)
        {
            _gameManager = gameManager;
            _containerDisplayManager = containerDisplayManager;
            _characterStatsDisplay = characterStatsDisplay;
            _party = party;
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
            for(int i = 0; i < _party.PartyDisplayOrder.Count; i++)
            {
                if (_party.PartyDisplayOrder.Count > PartyDisplayList.Count)
                {
                    CharacterPortrait portrait = Instantiate(CharacterPortraitPrefab, transform).GetComponent<CharacterPortrait>();
                    PartyDisplayList.Add(portrait);
                }
                var character = _party.Characters[_party.PartyDisplayOrder[i]];
                PartyDisplayList[i].SetupPortrait(guid: character.GuId, displayName: character.Stats.DisplayName, imagePath: character.Stats.PortraitPath);
                PartyDisplayList[i].PartyDisplay = this;
                if (_party.SelectedPartyMemberGuId == character.GuId)
                {
                    PartyDisplayList[i].IsSelected = true;
                    _characterStatsDisplay.CurrentCharacter = _party.SelectCharacter(character.GuId);
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
                    _characterStatsDisplay.CurrentCharacter = _party.SelectCharacter(characterGuid);
                }
                else
                    portrait.IsSelected = false;
            }
        }
    }
}
