using Data.Characters;
using Data.Items;
using InventoryQuest.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class EquipmentSlotDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        IInputManager _inputManager;
        ICharacter _character;

        [SerializeField] string _slotId;

        [SerializeField] Image backgroundSprite;
        [SerializeField] Image highlightSprite;
        [SerializeField] Image equippedItemSprite;
        [SerializeField] TextMeshProUGUI slotTypeText;

        public string SlotId => _slotId;

        [Inject]
        public void Init( IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        void Awake()
        {
            slotTypeText.text = SlotId.ToString();
        }

        public void SetCharacter(ICharacter character)
        {
            _character = character;
        }

        public void SetHighlightColor(Data.HighlightState state)
        {
            Color targetColor =
            state switch
            {
                Data.HighlightState.Highlight => UIPreferences.TextBuffColor,
                Data.HighlightState.Incorrect => UIPreferences.TextDeBuffColor,
                _ => Color.clear
            };
            highlightSprite.color = targetColor;
        }

        public void CheckIsOccupied()
        {
            if (_character is null) return;
            if (!_character.EquipmentSlots.ContainsKey(SlotId)) return;
            if (_character.EquipmentSlots[SlotId].EquippedItem is null)
            {
                backgroundSprite.color = Color.white;
                equippedItemSprite.color = Color.clear;
                equippedItemSprite.sprite = null;
            }
            else
            {
                backgroundSprite.color = Color.grey;
                equippedItemSprite.color = Color.white;
                equippedItemSprite.sprite = _character.EquipmentSlots[SlotId].EquippedItem.Sprite;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_inputManager.HoldingItem is not null)
            {
                Data.HighlightState squareState;
                var equipableItem = _inputManager.HoldingItem;
                if (equipableItem is not null && _character.EquipmentSlots[SlotId].IsValidPlacement(equipableItem))
                    squareState = Data.HighlightState.Highlight;
                else
                    squareState = Data.HighlightState.Incorrect;
                SetHighlightColor(squareState);
            }
            if (_character.EquipmentSlots[SlotId].EquippedItem is not null)
                _inputManager.ShowItemDetails(_character.EquipmentSlots[SlotId].EquippedItem);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHighlightColor(Data.HighlightState.Normal);
            _inputManager.HideItemDetails();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_inputManager.EquipmentSlotPointerClickHandler(eventData, _character, SlotId))
            {
                backgroundSprite.color = Color.white;
                equippedItemSprite.color = Color.clear;
                equippedItemSprite.sprite = null;
            }
            else
            {
                backgroundSprite.color = Color.grey;
                equippedItemSprite.color = Color.white;
                equippedItemSprite.sprite = (_character.EquipmentSlots[SlotId].EquippedItem as IItem).Sprite;
            }
        }
    }
}