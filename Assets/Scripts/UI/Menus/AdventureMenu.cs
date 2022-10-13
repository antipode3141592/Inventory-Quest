using Data.Items;
using InventoryQuest.Managers;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class AdventureMenu : Menu
    {
        IPartyManager _partyManager;
        IRewardManager _rewardManager;

        [SerializeField] ContainerDisplay characterContainerDisplay;
        [SerializeField] ContainerDisplay lootContainerDisplay;


        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
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
    }
}
