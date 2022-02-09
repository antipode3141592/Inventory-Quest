using Data;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class ContainerGridSquareDisplay : MonoBehaviour
    {
        SpriteRenderer _spriteRenderer;
        Container _container;

        public Coor Coordinates { get; set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();   
        }

        public void SetContainer(Container container)
        {
            _container = container;
        }

        public void SetColor(GridSquareState state)
        {
            Color targetColor =
            state switch
            {
                GridSquareState.Occupied => Color.grey,
                GridSquareState.Highlight => Color.green,
                GridSquareState.Incorrect => Color.red,
                _ => Color.white
            };
            _spriteRenderer.color = targetColor;
        }
    }
}