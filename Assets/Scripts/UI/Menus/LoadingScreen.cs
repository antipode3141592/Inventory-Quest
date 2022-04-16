using UnityEngine;

namespace InventoryQuest.UI.Menus
{
    public class LoadingScreen : MonoBehaviour
    {
        ScreenFader _screenFader;

        private void Awake()
        {
            _screenFader = GetComponent<ScreenFader>();
        }

        public void Fade() => _screenFader.FadeOff();

        public void FadeOn() => _screenFader.FadeOn();

    }


}
