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

        public void SetItem(Sprite sprite, int quantity = 0)
        {
            Image.sprite = sprite;

            Image.color = Color.white;
            Image.SetNativeSize();

            if (quantity > 0)
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