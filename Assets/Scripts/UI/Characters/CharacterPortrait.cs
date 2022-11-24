using Data.Characters;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class CharacterPortrait : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image background;
        [SerializeField] Image portrait;
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] HealthBar healthBar;

        ICharacter _character;

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

        public void SetupPortrait(string guid, string displayName, Sprite sprite, ICharacter character)
        {
            SetImage(sprite);
            SetName(displayName);
            CharacterGuid = guid;
            _character = character;
            SubscribeToHealthUpdate();
        }

        void SubscribeToHealthUpdate()
        {
            _character.OnStatsUpdated += CharacterStatsUpdatedHandler;
            SetHealthBar(1f);
        }

        void CharacterStatsUpdatedHandler(object sender, EventArgs e)
        {
            var character = sender as ICharacter;
            SetHealthBar(character.CurrentHealth / character.MaximumHealth);
        }

        void UnsubscribeFromHealthUpdate()
        {
            _character.OnStatsUpdated -= CharacterStatsUpdatedHandler;
        }



        void SetHealthBar(float percentage)
        {
            healthBar.SetForegroundWidth(percentage);
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