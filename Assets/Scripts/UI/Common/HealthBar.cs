using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] Image foreground;

        [SerializeField] Color backgroundColor = Color.red;
        [SerializeField] Color foregroundColor = Color.cyan;


        void Awake()
        {
            background.color = backgroundColor;
            foreground.color = foregroundColor;
            SetForegroundWidth(1f);
        }

        public void SetForegroundWidth(float fillPercentage)
        {
            foreground.rectTransform.localScale = new Vector3(x: Mathf.Clamp01(fillPercentage), y: 1f, z: 1f);
        }
    }
}
