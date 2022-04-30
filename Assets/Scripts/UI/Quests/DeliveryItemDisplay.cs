using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI.Quests
{
    public class DeliveryItemDisplay : MonoBehaviour
    {
        [SerializeField] Image ItemToDeliverImage;
        [SerializeField] TextMeshProUGUI ItemToDeliverText;

        public void SetItem(Sprite itemSprite, string itemName, int quantity = 1)
        {
            ItemToDeliverImage.sprite = itemSprite;
            if (quantity > 1)
                ItemToDeliverText.text = $"{itemName} x{quantity}";
            else
                ItemToDeliverText.text = $"{itemName}";
        }
    }
}
