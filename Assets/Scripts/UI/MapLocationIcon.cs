using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class MapLocationIcon: LocationIcon
    {
        [SerializeField] Button button;

        void Awake()
        {
            button.onClick.AddListener(LocationSelected);
        }

        public void LocationSelected()
        {

        }
    }
}