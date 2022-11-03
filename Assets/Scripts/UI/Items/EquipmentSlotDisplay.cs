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
        IGameManager _gameManager;
        ICharacter _character;

        [SerializeField] string _slotId;

        [SerializeField] Image backgroundSprite;
        [SerializeField] Image highlightSprite;
        [SerializeField] Image equippedItemSprite;
        [SerializeField] TextMeshProUGUI slotTypeText;


        public string SlotId => _slotId;

        [Inject]
        public void Init(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
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
                equippedItemSprite.sprite = null;
            }
            else
            {
                backgroundSprite.color = Color.grey;
                equippedItemSprite.sprite = (_character.EquipmentSlots[SlotId].EquippedItem as IItem).Sprite;
            }


        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_gameManager.CurrentState != GameStates.ItemHolding) return;
            Data.HighlightState squareState;
            var equipableItem = _gameManager.HoldingItem;
            if (equipableItem is not null && _character.EquipmentSlots[SlotId].IsValidPlacement(equipableItem)) { 
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
                    if (_character.EquipmentSlots[SlotId].TryUnequip(out var currentEquipment))
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
                    if (_character.EquipmentSlots[SlotId].TryEquip(out var previousItem, _gameManager.HoldingItem))
                    {
                        _gameManager.HoldingItem = previousItem as IItem;
                        if (_gameManager.HoldingItem is null) _gameManager.ChangeState(GameStates.Encounter);
                        else _gameManager.ChangeState(GameStates.ItemHolding);
                        backgroundSprite.color = Color.grey;
                        equippedItemSprite.color = Color.white;
                        equippedItemSprite.sprite = (_character.EquipmentSlots[SlotId].EquippedItem as IItem).Sprite;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}