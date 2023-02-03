using Data.Items;
using InventoryQuest.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class InventoryMenu : Menu
    {
        IPartyManager _partyManager;
        IInputManager _inputManager;
        IContainerManager _containerManager;

        [SerializeField] ContainerDisplay characterContainerDisplay;
        [SerializeField] ContainerDisplay lootContainerDisplay;
        [SerializeField] Button closeInventoryButton;
        [SerializeField] ItemDetailDisplay itemDetailDisplay;
        [SerializeField] ItemDetailDisplay heldItemDetailDisplay;

        [Inject]
        public void Init(IPartyManager partyManager, IInputManager inputManager, IContainerManager containerManager)
        {
            _partyManager = partyManager;
            _inputManager = inputManager;
            _containerManager = containerManager;
        }

        void Start()
        {
            closeInventoryButton.onClick.AddListener(CloseInventory);

            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;

            _containerManager.OnContainerSelected += OnContainerSelectedHandler;
            _containerManager.OnContainersDestroyed += OnContainersDestroyedHandler;
            
            _inputManager.OnItemUsed += OnItemUsedHandler;
        }

        public override void Show()
        {
            base.Show();
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

            _inputManager.ShowItemDetailsCommand += itemDetailDisplay.OnItemHeldHandler;
            _inputManager.HideItemDetailsCommand += itemDetailDisplay.OnItemPlacedHandler;
            _inputManager.OnItemHeld += heldItemDetailDisplay.OnItemHeldHandler;
            _inputManager.OnItemPlaced += heldItemDetailDisplay.OnItemPlacedHandler;

            itemDetailDisplay.ClearItemDetails();
            heldItemDetailDisplay.ClearItemDetails();

        }

        public override void Hide()
        {
            base.Hide();

            _inputManager.ShowItemDetailsCommand -= itemDetailDisplay.OnItemHeldHandler;
            _inputManager.HideItemDetailsCommand -= itemDetailDisplay.OnItemPlacedHandler;
            _inputManager.OnItemHeld -= heldItemDetailDisplay.OnItemHeldHandler;
            _inputManager.OnItemPlaced -= heldItemDetailDisplay.OnItemPlacedHandler;
        }

        void OnPartyMemberSelectedHandler(object sender, string e)
        {
            var container = _partyManager.CurrentParty.Characters[e].Backpack;
            if (container is null) return;
            characterContainerDisplay.SetContainer(container);
        }

        void OnContainerSelectedHandler(object sender, IContainer e)
        {
            lootContainerDisplay.SetContainer(e);
        }

        void OnContainersDestroyedHandler(object sender, EventArgs e)
        {
            lootContainerDisplay.SetContainer(null);
        }

        void OnItemUsedHandler(object sender, IItem e)
        {
            itemDetailDisplay.DisplayItemDetails(e);
        }

        public void CloseInventory()
        {
            _inputManager.CloseInventory();
        }
    }
}
