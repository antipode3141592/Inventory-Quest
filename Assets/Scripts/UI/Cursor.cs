using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

namespace InventoryQuest.UI
{
    public class Cursor : MonoBehaviour
    {
        public Image cursorIcon;
        
        public Image itemIcon;

        public RectTransform RectTransform => transform as RectTransform;
    }
}