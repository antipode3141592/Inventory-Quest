using System.Collections.Generic;
using UnityEngine;


namespace InventoryQuest.UI.Menus
{
    public abstract class Menu : MonoBehaviour, IMenu
    {
        protected readonly Vector3 offset = new Vector3(0f, 10000f, 0f);
        [SerializeField] protected Transform menuTransform;
        protected Vector3 _originalPosition;
        protected List<IOnMenuShow> _subMenus = new();

        protected virtual void Awake()
        {
            _originalPosition = menuTransform.position;
            _subMenus.AddRange(GetComponentsInChildren<IOnMenuShow>());
        }

        public virtual void Hide()
        {
            menuTransform.position = _originalPosition + offset;
        }

        public virtual void Show()
        {
            menuTransform.position = _originalPosition;
            foreach (var menu in _subMenus)
                menu.OnShow();
        }
    }
}
