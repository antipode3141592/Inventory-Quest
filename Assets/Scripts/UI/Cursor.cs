using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class Cursor : MonoBehaviour
    {
        public Image cursorIcon;
        
        public Image itemIcon;

        public RectTransform RectTransform => transform as RectTransform;
    }
}