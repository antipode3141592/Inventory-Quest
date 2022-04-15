using Data;
using Data.Items;
using InventoryQuest.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class ContainerGridSquareDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        GameManager _gameManager;
        IContainer _container;

        [SerializeField]
        Image backgroundSprite;
        [SerializeField]
        Image highlightSprite;

        bool _isOccupied;
        public bool IsOccupied { 
            get { return _isOccupied; } 
            set { 
                backgroundSprite.color = value ? Color.grey : Color.white;
                _isOccupied = value;
            } 
        }

        public float Width => 32f;

        HighlightState _highlightState;
        public HighlightState CurrentState {
            get { return _highlightState; }
            set {
                SetHighlightColor(value);
                _highlightState = value;
            } 
        }
        public Coor Coordinates { get; set; }

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void SetHighlightColor(HighlightState state)
        {
            Color targetColor =
            state switch
            {
                HighlightState.Highlight => Color.green,
                HighlightState.Incorrect => Color.red,
                _ => Color.clear
            };
            highlightSprite.color = targetColor;
        }

        public void SetContainer(IContainer container)
        {
            _container = container;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_gameManager.CurrentState != GameStates.ItemHolding) return;
            var squareState = _container.IsValidPlacement(_gameManager.HoldingItem, Coordinates) ? HighlightState.Highlight : HighlightState.Incorrect;
            SetHighlightColor(squareState);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHighlightColor(HighlightState.Normal);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (_gameManager.CurrentState)
            {
                case GameStates.Encounter:
                    if (_container.TryTake(out var item, Coordinates))
                    {
                        _gameManager.HoldingItem = item;
                        _gameManager.ChangeState(GameStates.ItemHolding);
                    }
                    break;
                case GameStates.ItemHolding:
                    if (_container.TryPlace(_gameManager.HoldingItem, Coordinates))
                    {
                        _gameManager.HoldingItem = null;
                        _gameManager.ChangeState(GameStates.Encounter);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}