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
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
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
                PartyDisplayList[i].IsSelected = MyParty.SelectedPartyMemberGuId == character.GuId; 

            }
        }
        
        public void PartyMemberSelected(string characterGuid)
        {
            foreach (var portrait in PartyDisplayList)
            {
                portrait.IsSelected = portrait.CharacterGuid == characterGuid;
            }
        }
    }
}
