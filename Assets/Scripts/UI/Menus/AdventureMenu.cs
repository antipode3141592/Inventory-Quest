using Data.Items;
using InventoryQuest.Managers;
using InventoryQuest.Managers.States;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class AdventureMenu : Menu
    {
        IPartyManager _partyManager;
        IRewardManager _rewardManager;
        IEncounterManager _encounterManager;

        [SerializeField] ContainerDisplay characterContainerDisplay;
        [SerializeField] ContainerDisplay lootContainerDisplay;
        [SerializeField] PressAndHoldButton continueButton;

        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IEncounterManager encounterManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _encounterManager = encounterManager;
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
            characterContainerDisplay.MyContainer = container;
        }

        void OnLootPileSelectedHandler(object sender, Container e)
        {
            lootContainerDisplay.MyContainer = e;
        }

        void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            lootContainerDisplay.MyContainer = null;
        }

        void Continue(object sender, EventArgs e)
        {
            if (_encounterManager.CurrentStateName == typeof(ManagingInventory).Name)
                _encounterManager.ManagingInventory.Continue();
            if (_encounterManager.CurrentStateName == typeof(CleaningUp).Name)
                _encounterManager.CleaningUp.Continue();
        }
    }
}
