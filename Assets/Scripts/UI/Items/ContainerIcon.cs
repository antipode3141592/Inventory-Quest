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

        public void SetContainerIcon(string guid, string imagePath, IContainersDisplay containersDisplay)
        {
            SetImage(imagePath);
            ContainerGuid = guid;
            _containersDisplay = containersDisplay;
        }

        void SetImage(string path)
        {
            icon.sprite = Resources.Load<Sprite>(path);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _containersDisplay.ContainerSelected(ContainerGuid);
        }
    }
}
