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

        private Party myParty;
        public Party MyParty
        {
            get => myParty;
            set
            {
                myParty = value;
                SetPortraits();
            }
        }
        [Inject]
        public void Init(GameManager gameManager, ContainerDisplayManager containerDisplayManager, CharacterStatsDisplay characterStatsDisplay)
        {
            _gameManager = gameManager;
            _containerDisplayManager = containerDisplayManager;
            _characterStatsDisplay = characterStatsDisplay;
        }

        void Awake()
        {
            PartyDisplayList = new List<CharacterPortrait>();
            
        }

        private void Start()
        {
            MyParty = _gameManager.CurrentParty;
        }

        public void SetPortraits()
        {
            for(int i = 0; i < MyParty.PartyDisplayOrder.Count; i++)
            {
                if (MyParty.PartyDisplayOrder.Count > PartyDisplayList.Count)
                {
                    CharacterPortrait portrait = Instantiate(CharacterPortraitPrefab, transform).GetComponent<CharacterPortrait>();
                    PartyDisplayList.Add(portrait);
                }
                var character = MyParty.Characters[MyParty.PartyDisplayOrder[i]];
                PartyDisplayList[i].SetupPortrait(guid: character.GuId, displayName: character.Stats.DisplayName, imagePath: character.Stats.PortraitPath);
                PartyDisplayList[i].PartyDisplay = this;
                if (MyParty.SelectedPartyMemberGuId == character.GuId)
                {
                    PartyDisplayList[i].IsSelected = true;
                    _characterStatsDisplay.CurrentCharacter = MyParty.SelectCharacter(character.GuId);
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
                    _characterStatsDisplay.CurrentCharacter = MyParty.SelectCharacter(characterGuid);
                }
                else
                    portrait.IsSelected = false;
            }
        }
    }
}
