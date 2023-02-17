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
        Color nameTextColor;
        [SerializeField] HealthBar healthBar;

        ICharacter _character;

        PartyDisplay partyDisplay;

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

        void Awake()
        {
            nameTextColor = nameText.color;
        }

        void SetImage(Sprite sprite)
        {
            portrait.sprite = sprite;
        }

        void SetName(string name)
        {
            nameText.text = name;
        }

        public void SetupPortrait(PartyDisplay partyDisplay, string guid, string displayName, Sprite sprite, ICharacter character)
        {
            SetImage(sprite);
            SetName(displayName);
            CharacterGuid = guid;
            _character = character;
            SubscribeToHealthUpdate();
            this.partyDisplay = partyDisplay;
        }

        void SubscribeToHealthUpdate()
        {
            _character.DamageTaken += CharacterDamaged;
            _character.DamageHealed += CharacterHealed;
            UpdateHealthBar(_character);
        }

        void UnsubscribeFromHealthUpdate()
        {
            _character.DamageTaken -= CharacterDamaged;
            _character.DamageHealed -= CharacterHealed;
        }

        void CharacterDamaged(object sender, int e)
        {
            UpdateHealthBar(_character);
        }

        void CharacterHealed(object sender, int e)
        {
            UpdateHealthBar(_character);
        }
        
        void UpdateHealthBar(ICharacter character)
        {
            SetHealthBar((float)character.CurrentHealth / (float)character.MaximumHealth);
        }

        void SetHealthBar(float percentage)
        {
            healthBar.SetForegroundWidth(percentage);
        }

        public void SelectPartyMember()
        {
            Debug.Log($"SelectPartyMember() called on character {CharacterGuid}", gameObject);
            partyDisplay.PartyMemberSelected(CharacterGuid);
        }

        public void ChangePartyMemberName(string name)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectPartyMember();
        }

        public void Show()
        {
            background.color = Color.white;
            portrait.color = Color.white;
            nameText.color = nameTextColor;
            healthBar.Show();
        }

        public void Hide()
        {
            background.color = Color.clear;
            portrait.color = Color.clear;
            nameText.color = Color.clear;
            healthBar.Hide();
        }
    }
}