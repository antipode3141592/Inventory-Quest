using InventoryQuest.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class LocationMenu : Menu
    {
        IAdventureManager _adventureManager;
        MenuController _menuController;

        [SerializeField] string selectedPath = "intro_path";

        [SerializeField] TextMeshProUGUI PathNameText;
        [SerializeField] Button StartAdventureButton;

        [Inject]
        public void Init(IAdventureManager adventureManager, MenuController menuController)
        {
            _adventureManager = adventureManager;
            _menuController = menuController;
        }

        private void Awake()
        {
            StartAdventureButton.onClick.AddListener(ChooseSelectedPath);
            PathNameText.text = selectedPath;
        }

        void ChooseSelectedPath()
        {
            _menuController.OpenMenu(typeof(AdventureMenu));
            _adventureManager.ChoosePath(selectedPath);
        }
    }
}