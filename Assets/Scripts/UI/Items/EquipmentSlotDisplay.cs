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
        GameManager _gameManager;
        PlayableCharacter _character;

        [SerializeField]
        EquipmentSlotType _slotType;

        [SerializeField]
        Image backgroundSprite;
        [SerializeField]
        Image highlightSprite;
        [SerializeField]
        Image equippedItemSprite;
        [SerializeField]
        TextMeshProUGUI slotTypeText;


        public EquipmentSlotType SlotType => _slotType;

        [Inject]
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            slotTypeText.text = SlotType.ToString();
        }

        public void SetCharacter(PlayableCharacter character)
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
            if (_character.EquipmentSlots[SlotType].EquippedItem is null)
            {
                backgroundSprite.color = Color.white;
                equippedItemSprite.sprite = null;
            }
            else
            {
                backgroundSprite.color = Color.grey;
                equippedItemSprite.sprite = (_character.EquipmentSlots[SlotType].EquippedItem as IItem).Sprite;
            }


        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_gameManager.CurrentState != GameStates.ItemHolding) return;
            Data.HighlightState squareState;
            if (_character.EquipmentSlots[SlotType].IsValidPlacement(_gameManager.HoldingItem as IEquipable)) { 
                squareState = Data.HighlightState.Highlight;
                
            } 
            else
            {
                squareState = Data.HighlightState.Incorrect;
            }
            SetHighlightColor(squareState);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHighlightColor(Data.HighlightState.Normal);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"OnPointerClick() for {gameObject.name}");
            switch (_gameManager.CurrentState)
            {
                case GameStates.Encounter:
                    if (_character.EquipmentSlots[SlotType].TryUnequip(out var currentEquipment))
                    {
                        if (currentEquipment is null) return;
                        _gameManager.HoldingItem = currentEquipment as IItem;
                        _gameManager.ChangeState(GameStates.ItemHolding);
                        backgroundSprite.color = Color.white;
                        equippedItemSprite.color = Color.white;
                        equippedItemSprite.sprite = null;
                    }
                    break;
                case GameStates.ItemHolding:
                    if (_character.EquipmentSlots[SlotType].TryEquip(out var previousItem, _gameManager.HoldingItem as IEquipable))
                    {
                        _gameManager.HoldingItem = previousItem as IItem;
                        if (_gameManager.HoldingItem is null) _gameManager.ChangeState(GameStates.Encounter);
                        else _gameManager.ChangeState(GameStates.ItemHolding);
                        backgroundSprite.color = Color.grey;
                        equippedItemSprite.color = Color.white;
                        equippedItemSprite.sprite = (_character.EquipmentSlots[SlotType].EquippedItem as IItem).Sprite;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}