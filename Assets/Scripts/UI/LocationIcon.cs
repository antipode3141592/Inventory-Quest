using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class LocationIcon : MonoBehaviour
    {

        [SerializeField] Image Icon;
        [SerializeField] TextMeshProUGUI LocationName;
        
        public void Set(Sprite locationSprite, string locationName)
        {
            Icon.sprite = locationSprite;
            LocationName.text = locationName;
        }
    }
}