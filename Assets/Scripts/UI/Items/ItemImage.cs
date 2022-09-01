using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class ItemImage : MonoBehaviour
    {
        public Image Image;

        [SerializeField] Image quantityImage;
        [SerializeField] TextMeshProUGUI quantityText;
        [SerializeField] Color quantityVisible;


        void Awake()
        {
            quantityImage.color = Color.clear;
            quantityText.text = "";
        }

        public void SetItem(Sprite sprite, int quantity = 1)
        {
            Image.sprite = sprite;

            Image.color = Color.white;
            Image.SetNativeSize();

            if (quantity > 1)
            {
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