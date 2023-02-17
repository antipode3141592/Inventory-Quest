using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class NewsItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Image background;

        Color backgroundBaseColor;

        public bool IsActive;

        void Awake()
        {
            backgroundBaseColor = background.color;
        }

        public void Show()
        {
            text.color = Color.white;
            background.color = backgroundBaseColor;
            IsActive = true;
        }

        public void Hide()
        {
            text.text = "";
            text.color = Color.clear;
            background.color = Color.clear;
        }

        public void SetText(string newsText)
        {
            text.text = newsText;
        }
    }
}
