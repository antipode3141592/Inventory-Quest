using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class ItemImage : MonoBehaviour
    {
        public Image Image;
        

        protected string itemGuId;

        [SerializeField] Image quantityImage;
        [SerializeField] TextMeshProUGUI quantityText;
        [SerializeField] Color quantityVisible;

        public string ItemGuId => itemGuId;

        void Awake()
        {
            quantityImage.color = Color.clear;
            quantityText.text = "";
        }

        public void SetItem(string itemGuId, Sprite sprite, int quantity = 1, Vector3 anchorPosition = default)
        {
            Image.sprite = sprite;
            this.itemGuId = itemGuId;
            Image.color = Color.white;
            Image.SetNativeSize();

            if (quantity > 1)
            {
                quantityImage.rectTransform.anchoredPosition = anchorPosition;
                quantityImage.color = quantityVisible;
                quantityText.text = $"{quantity}";
            }
            else
            {
                quantityImage.color = Color.clear;
                quantityText.text = "";
            }
        }
    }
}