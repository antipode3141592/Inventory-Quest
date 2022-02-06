using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Data;

namespace InventoryQuest.UI
{
    public class PartyDisplay: MonoBehaviour
    {
        [SerializeField]
        GameObject CharacterPortraitPrefab;

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

        void Awake()
        {
            PartyDisplayList = new List<CharacterPortrait>();

        }

        public void SetPortraits()
        {
            for(int i = 0; i < MyParty.PartyDisplayOrder.Count; i++)
            {
                CharacterPortrait portrait = Instantiate(CharacterPortraitPrefab, transform).GetComponent<CharacterPortrait>();
                portrait.SetName(MyParty.Characters[MyParty.PartyDisplayOrder[i]].Stats.DisplayName);
                portrait.SetImage(MyParty.Characters[MyParty.PartyDisplayOrder[i]].Stats.PortraitPath);
            }
        }

        
    }
}
