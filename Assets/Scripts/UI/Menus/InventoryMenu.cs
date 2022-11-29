using Data.Items;
using InventoryQuest.Managers;
using InventoryQuest.Managers.States;
using System;
using UnityEngine;
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
        [SerializeField] PressAndHoldButton continueButton;

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IInputManager inputManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _inputManager = inputManager;
        }

        void Start()
        {
            continueButton.OnPointerHoldSuccess += Continue;
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
            _rewardManager.OnRewardsCleared += OnRewardsClearedHandler;
            _rewardManager.OnPileSelected += OnLootPileSelectedHandler;
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();

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

        void Continue(object sender, EventArgs e)
        {
            _inputManager.CloseInventory();
        }
    }
}
