using Data;
using Data.Items;
using InventoryQuest.Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using HighlightState = Data.HighlightState;

namespace InventoryQuest.UI
{
    public class ContainerGridSquareDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        IGameManager _gameManager;
        IInputManager _inputManager;
        IContainer _container;

        [SerializeField] Image backgroundSprite;
        [SerializeField] Image highlightSprite;
        [SerializeField] Image matchingHighlightSprite;
        [SerializeField] ColorSettings colorSettings;


        bool _isOccupied;
        public bool IsOccupied
        {
            get { return _isOccupied; }
            set
            {
                backgroundSprite.color = value ? colorSettings.GridOccupiedColor : colorSettings.GridUnoccupiedColor;
                _isOccupied = value;
            }
        }

        public float Width => 32f;

        HighlightState _highlightState;
        [SerializeField] Coor coordinates;

        public HighlightState CurrentState
        {
            get { return _highlightState; }
            set
            {
                SetHighlightColor(value);
                _highlightState = value;
            }
        }
        public Coor Coordinates { get => coordinates; set => coordinates = value; }

        public event EventHandler<Coor> GridSquarePointerEntered;
        public event EventHandler<Coor> GridSquarePointerExited;
        public event EventHandler<PointerEventData> GridSquarePointerClicked;

        void Awake() 
        {
            _gameManager = FindObjectOfType<GameManager>();
            _inputManager = FindObjectOfType<InputManager>();
        }

        public void SetHighlightColor(HighlightState state, float timer = 0f)
        {
            Color targetColor =
            state switch
            {
                HighlightState.Highlight => colorSettings.TextBuffColor,
                HighlightState.Incorrect => colorSettings.TextDeBuffColor,
                HighlightState.Match => colorSettings.MatchColor,
                _ => Color.clear
            };
            highlightSprite.color = targetColor;
            if (timer > 0f)
                StartCoroutine(ResetHighlightOverTime(timer, highlightSprite));
        }

        public void SetMatchingHighlightColor(HighlightState state, float timer = 0f)
        {
            Color targetColor =
            state switch
            {
                HighlightState.Highlight => colorSettings.TextBuffColor,
                HighlightState.Incorrect => colorSettings.TextDeBuffColor,
                HighlightState.Match => colorSettings.MatchColor,
                _ => Color.clear
            };
            matchingHighlightSprite.color = targetColor;
            if (timer > 0f)
                StartCoroutine(ResetHighlightOverTime(timer, matchingHighlightSprite));
        }

        IEnumerator ResetHighlightOverTime(float timer, Image image)
        {
            yield return new WaitForSeconds(timer);
            image.color = Color.clear;
        }

        public void SetContainer(IContainer container)
        {
            _container = container;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GridSquarePointerEntered?.Invoke(this, Coordinates);

            if (_gameManager.CurrentState != GameStates.ItemHolding) return;
            var squareState = _container.IsValidPlacement(_inputManager.HoldingItem, Coordinates) ? HighlightState.Highlight : HighlightState.Incorrect;
            SetHighlightColor(squareState);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GridSquarePointerExited?.Invoke(this, Coordinates);
            SetHighlightColor(HighlightState.Normal);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GridSquarePointerClicked?.Invoke(this, eventData);
            if (eventData.button == PointerEventData.InputButton.Left)

            switch (_gameManager.CurrentState)
            {
                case GameStates.Encounter:
                    if (_container.TryTake(out var item, Coordinates))
                    {
                        _inputManager.HoldingItem = item;
                        _gameManager.ChangeState(GameStates.ItemHolding);
                    }
                    break;
                case GameStates.ItemHolding:
                    if (_container.TryPlace(_inputManager.HoldingItem, Coordinates))
                    {
                        _inputManager.HoldingItem = null;
                        _gameManager.ChangeState(GameStates.Encounter);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}