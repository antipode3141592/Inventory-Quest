using InventoryQuest.UI.Menus;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class ContainerIcon : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] Image background;
        [SerializeField] Image icon;
        public string ContainerGuid { get; protected set; }

        IContainersDisplay _containersDisplay;

        bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                background.color = value ? Color.green : Color.clear;
                isSelected = value;
            }
        }

        public void SetContainerIcon(string guid, Sprite image, IContainersDisplay containersDisplay)
        {
            SetImage(image);
            ContainerGuid = guid;
            _containersDisplay = containersDisplay;
        }

        void SetImage(Sprite sprite)
        {
            icon.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"OnPointerClick received for icon {gameObject.name}");
            _containersDisplay.ContainerSelected(ContainerGuid);
        }
    }
}
