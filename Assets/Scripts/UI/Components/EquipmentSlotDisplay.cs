using Data;
using Data.Interfaces;
using InventoryQuest.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace InventoryQuest.UI
{
    public class EquipmentSlotDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        GameManager _gameManager;
        Character _character;

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

        public void SetCharacter(Character character)
        {
            _character = character;
        }

        public void SetHighlightColor(Data.HighlightState state)
        {
            Color targetColor =
            state switch
            {
                Data.HighlightState.Highlight => Color.green,
                Data.HighlightState.Incorrect => Color.red,
                _ => Color.clear
            };
            highlightSprite.color = targetColor;
        }

        public void CheckIsOccupied()
        {
            if (_character is null) return;
            backgroundSprite.color = _character.EquipmentSlots[SlotType].EquippedItem is null ? Color.white : Color.grey;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"OnPointerDown() for {gameObject.name}");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log($"OnPointerUp() for {gameObject.name}");
            switch (_gameManager.CurrentState)
            {
                case GameStates.Default:
                    if(_character.EquipmentSlots[SlotType].TryUnequip(out var currentEquipment)) {
                        if (currentEquipment is null) return;
                        _gameManager.HoldingItem = currentEquipment as IItem;
                        _gameManager.ChangeState(GameStates.HoldingItem);
                        backgroundSprite.color = Color.white;
                    }
                    break;
                case GameStates.HoldingItem:
                    if (_character.EquipmentSlots[SlotType].TryEquip(out var previousItem, _gameManager.HoldingItem as IEquipable))
                    {
                        _gameManager.HoldingItem = previousItem as IItem;
                        if (_gameManager.HoldingItem is null) _gameManager.ChangeState(GameStates.Default);
                        else _gameManager.ChangeState(GameStates.HoldingItem);
                        backgroundSprite.color = Color.grey;
                    }
                    break;
                default:
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_gameManager.CurrentState != GameStates.HoldingItem) return;
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

        
    }
}