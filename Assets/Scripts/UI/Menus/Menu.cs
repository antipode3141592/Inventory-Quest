using UnityEngine;


namespace InventoryQuest.UI.Menus
{
    public abstract class Menu : MonoBehaviour, IMenu
    {
        protected readonly Vector3 offset = new Vector3(0f, 10000f, 0f);

        [SerializeField] protected Transform menuTransform;
        
        protected Vector3 _originalPosition;

        protected virtual void Awake()
        {
            _originalPosition = menuTransform.position;
        }

        public virtual void Hide()
        {
            menuTransform.position = _originalPosition + offset;
        }

        public virtual void Show()
        {
            menuTransform.position = _originalPosition;
        }
    }
}
