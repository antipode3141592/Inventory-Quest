using InventoryQuest.Managers;
using System;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class HarvestMenu: Menu
    {
        IHarvestManager _harvestManager;

        PressAndHoldButton continueButton;

        [Inject]
        public void Init(IHarvestManager harvestManager)
        {
            _harvestManager = harvestManager;
        }

        protected override void Awake()
        {
            base.Awake();
            continueButton = GetComponentInChildren<PressAndHoldButton>();
        }

        void Start()
        {
            continueButton.OnPointerHoldSuccess += OnContinueRequested;
        }

        void OnContinueRequested(object sender, EventArgs e)
        {
            _harvestManager.ResolvingHarvest.Resolve();
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
