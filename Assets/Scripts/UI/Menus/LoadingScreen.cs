using UnityEngine;

namespace InventoryQuest.UI.Menus
{
    public class LoadingScreen : MonoBehaviour
    {
        ScreenFader _screenFader;

        void Awake()
        {
            _screenFader = GetComponent<ScreenFader>();
        }

        public void FadeOff() => _screenFader.FadeOff();

        public void FadeOn() => _screenFader.FadeOn();

    }


}
