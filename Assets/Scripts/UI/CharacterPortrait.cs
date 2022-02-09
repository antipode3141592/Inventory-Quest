using Rewired;
using Rewired.Integration.UnityUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class CharacterPortrait : MonoBehaviour
    {
        [SerializeField]
        Image background;
        [SerializeField]
        Image portrait;
        [SerializeField]
        TextMeshProUGUI nameText;

        public PartyDisplay PartyDisplay;

        Player player;
        int playerId = 0;

        public string CharacterGuid { get; private set; }

        bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                background.color = value ? Color.green : Color.clear;
                isSelected = value;
            }
        }

        private void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
        }

        //ex path: Portraits/Enemy 01-1  (exclude leading slash and filetype)
        void SetImage(string path)
        {
            portrait.sprite = Resources.Load<Sprite>(path);
        }

        public void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetupPortrait(string guid, string displayName, string imagePath)
        {
            SetImage(imagePath);
            SetName(displayName);
            CharacterGuid = guid;
        }

        public void SelectPartyMember()
        {
            Debug.Log($"SelectPartyMember() called on character {CharacterGuid}", gameObject);
            PartyDisplay.PartyMemberSelected(CharacterGuid);

        }

        public void ChangePartyMemberName(string name)
        {

        }
    }
}