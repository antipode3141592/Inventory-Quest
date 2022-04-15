using InventoryQuest.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class LocationMenu : Menu
    {
        AdventureManager _adventureManager;
        MenuManager _menuManager;

        [SerializeField] string selectedPath = "intro_path";

        [SerializeField] TextMeshProUGUI PathNameText;
        [SerializeField] Button StartAdventureButton;

        [Inject]
        public void Init(AdventureManager adventureManager, MenuManager menuManager)
        {
            _adventureManager = adventureManager;
            _menuManager = menuManager;
        }

        private void Awake()
        {
            StartAdventureButton.onClick.AddListener(ChooseSelectedPath);
            PathNameText.text = selectedPath;
        }

        void ChooseSelectedPath()
        {
            _menuManager.OpenMenu(typeof(AdventureMenu));
            _adventureManager.ChoosePath(selectedPath);
        }
    }
}