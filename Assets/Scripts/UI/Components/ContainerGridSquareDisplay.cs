using Data;
using InventoryQuest.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class ContainerGridSquareDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        GameManager _gameManager;
        Container _container;

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

        public void SetContainer(Container container)
        {
            _container = container;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"OnPointerDown() on {gameObject.name} with coor: {Coordinates}");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log($"OnPointerup() on {gameObject.name} with coor: {Coordinates}");
            switch (_gameManager.CurrentState)
            {
                case GameStates.Default:
                    if (_container.TryTake(out var item, Coordinates))
                    {
                        _gameManager.HoldingItem = item;
                        _gameManager.ChangeState(GameStates.HoldingItem);
                    }
                    break;
                case GameStates.HoldingItem:
                    if (_container.TryPlace(_gameManager.HoldingItem, Coordinates))
                    {
                        _gameManager.HoldingItem = null;
                        _gameManager.ChangeState(GameStates.Default);
                    }
                    break;
                default:
                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_gameManager.CurrentState != GameStates.HoldingItem) return;
            var squareState = _container.IsValidPlacement(_gameManager.HoldingItem, Coordinates) ? HighlightState.Highlight : HighlightState.Incorrect;
            SetHighlightColor(squareState);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetHighlightColor(HighlightState.Normal);
        }   
    }
}