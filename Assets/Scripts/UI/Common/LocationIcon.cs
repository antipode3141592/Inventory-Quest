using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class LocationIcon : MonoBehaviour
    {

        [SerializeField] Image Icon;
        [SerializeField] TextMeshProUGUI LocationName;
        protected string _locationName;
        
        public void Set(Sprite locationSprite, string locationName)
        {
            Icon.sprite = locationSprite;
            LocationName.text = locationName;
            _locationName = locationName;
        }
    }
}