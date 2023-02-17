using Data;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using HighlightState = Data.HighlightState;

namespace InventoryQuest.UI
{
    public class ContainerGridSquareDisplay : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Image backgroundSprite;
        [SerializeField] Image highlightSprite;
        [SerializeField] Image matchingHighlightSprite;
        [SerializeField] ColorSettings colorSettings;

        Coor coordinates;
        bool _isPointerHovering = false;

        public bool IsActive;
        public bool IsPointerHovering => _isPointerHovering;

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

        public Coor Coordinates { get => coordinates; set => coordinates = value; }

        public event EventHandler<PointerEventData> GridSquarePointerEntered;
        public event EventHandler<PointerEventData> GridSquarePointerExited;
        public event EventHandler<PointerEventData> GridSquarePointerClicked;

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerHovering = true;
            GridSquarePointerEntered?.Invoke(this, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerHovering = false;
            GridSquarePointerExited?.Invoke(this, eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GridSquarePointerClicked?.Invoke(this, eventData);
        }

        public void Show() 
        {
            IsActive = true;
            backgroundSprite.color = colorSettings.GridUnoccupiedColor;
        }

        public void Hide() 
        {
            IsActive = false;
            backgroundSprite.color = Color.clear;
        }

    }
}