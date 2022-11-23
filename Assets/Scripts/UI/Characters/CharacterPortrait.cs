using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class CharacterPortrait : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        Image background;
        [SerializeField]
        Image portrait;
        [SerializeField]
        TextMeshProUGUI nameText;

        public PartyDisplay PartyDisplay;

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

        void SetImage(Sprite sprite)
        {
            portrait.sprite = sprite;
        }

        void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetupPortrait(string guid, string displayName, Sprite image)
        {
            SetImage(image);
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

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectPartyMember();
        }
    }
}