using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class LootIcon : MonoBehaviour, IPointerClickHandler
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

        public void SetupLootIcon(string guid, string imagePath)
        {
            SetImage(imagePath);
            ContainerGuid = guid;

        }

        void SetImage(string path)
        {
            icon.sprite = Resources.Load<Sprite>(path);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"LootPile {ContainerGuid} Selected", gameObject);
            LootListDisplay.LootPileSelected(ContainerGuid);
        }
    }
}
