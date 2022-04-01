using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class LootIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        Image background;
        [SerializeField]
        Image icon;
        [SerializeField]
        public string ContainerGuid { get; private set; }

        public LootListDisplay LootListDisplay;

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

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log($"LootPile {ContainerGuid} Selected", gameObject);
            LootListDisplay.LootPileSelected(ContainerGuid);
        }

        public void SetupLootIcon(string guid, string imagePath)
        {
            SetImage(imagePath);
            ContainerGuid = guid;

        }

        void SetImage(string path)
        {
            icon.sprite = Resources.Load<Sprite>(path);
        }

    }
}
