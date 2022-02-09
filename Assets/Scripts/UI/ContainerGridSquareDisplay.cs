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
    }
}