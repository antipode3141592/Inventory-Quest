using Data;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class ContainerGridSquareDisplay : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer backgroundSprite;
        [SerializeField]
        SpriteRenderer highlightSprite;

        bool _isOccupied;
        public bool IsOccupied { 
            get { return _isOccupied; } 
            set { 
                backgroundSprite.color = value ? Color.grey : Color.white;
                _isOccupied = value;
            } 
        }

        HighlightState _highlightState;
        public HighlightState CurrentState {
            get { return _highlightState; }
            set {
                SetHighlightColor(value);
                _highlightState = value;
            } 
        }
        public Coor Coordinates { get; set; }

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
    }
}