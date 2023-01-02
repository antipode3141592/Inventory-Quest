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
        IRewardManager _rewardManager;
        IInputManager _inputManager;

        [SerializeField] ContainerDisplay characterContainerDisplay;
        [SerializeField] ContainerDisplay lootContainerDisplay;
        [SerializeField] Button closeInventoryButton;

        [SerializeField] ItemDetailDisplay itemDetailDisplay;
        [SerializeField] ItemDetailDisplay heldItemDetailDisplay;

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IInputManager inputManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _inputManager = inputManager;
        }

        void Start()
        {
            closeInventoryButton.onClick.AddListener(CloseInventory);

            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
            _rewardManager.OnRewardsCleared += OnRewardsClearedHandler;
            _rewardManager.OnPileSelected += OnLootPileSelectedHandler;
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

        void OnLootPileSelectedHandler(object sender, IContainer e)
        {
            lootContainerDisplay.SetContainer(e);
        }

        void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            lootContainerDisplay.SetContainer(null);
        }

        public void CloseInventory()
        {
            _inputManager.CloseInventory();
        }
    }
}
