using InventoryQuest.Managers;
using System;
using Zenject;
using UnityEngine;
using Data.Items;

namespace InventoryQuest.UI.Menus
{
    public class HarvestMenu: Menu
    {
        IHarvestManager _harvestManager;
        IPartyManager _partyManager;

        [SerializeField] ContainerDisplay harvestContainerDisplay;
        [SerializeField] ContainerDisplay characterContainerDisplay;

        [SerializeField] PressAndHoldButton continueButton;

        [Inject]
        public void Init(IHarvestManager harvestManager, IPartyManager partyManager)
        {
            _harvestManager = harvestManager;
            _partyManager = partyManager;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            continueButton.OnPointerHoldSuccess += OnContinueRequested;

            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
            _harvestManager.OnPileSelected += OnHarvestPileSelected;
        }

        void OnHarvestPileSelected(object sender, Container e)
        {
            if (e is null) return;
            harvestContainerDisplay.MyContainer = e;
        }

        void OnPartyMemberSelectedHandler(object sender, string e)
        {
            var container = _partyManager.CurrentParty.Characters[e].Backpack;
            if (container is null) return;
            characterContainerDisplay.MyContainer = container;
        }

        void OnContinueRequested(object sender, EventArgs e)
        {
            _harvestManager.Harvesting.Continue();
        }

        public override void Show()
        {
            base.Show();

        }

        public override void Hide()
        {
            base.Hide();

        }
    }
}
